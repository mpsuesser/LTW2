using System.Linq;
using UnityEngine;

public class BuildCommand : ICommand {
    public CommandState State { get; private set; }
    
    private int RequestingClientID { get; }
    private ServerBuilder Builder { get; }
    private TowerType Type { get; }
    private MazeGridCell[] Cells { get; }
    private Vector3 BuildLocation { get; }
    private int GoldCost { get; }
    
    private TowerBuildLocationTrigger Trigger { get; set;  }
    
    public BuildCommand(
        int requestingClientID,
        ServerBuilder builder,
        TowerType towerType,
        MazeGridCell[] cells,
        int goldCost
    ) {
        RequestingClientID = requestingClientID;
        Builder = builder;
        Type = towerType;
        Cells = cells;
        GoldCost = goldCost;
        
        Vector3 midpoint = ServerUtil.GetMidpointOfTransforms(
            cells.Select(cell => cell.transform).ToArray()
        );
        BuildLocation = new Vector3(
            midpoint.x, 
            (float)TowerConstants.PlacementHeight[Type],
            midpoint.z
        );
        
        State = CommandState.PendingExecution;
    }
    public void PrepareForExecution() {
        CheckForCellBlockage();
        CheckForSufficientGold();
        CheckForMazeBlockage();
        if (State != CommandState.PendingExecution) {
            return;
        }
        
        Builder.ActiveLane.DeductGold(GoldCost);
        
        Trigger = TowerBuildLocationTrigger.Create(
            Builder,
            BuildLocation
        );
        Trigger.OnBuilderReachedTowerBuildLocation += BuildTower;
        
        Builder.Navigation.SetDestination(BuildLocation);
        
        State = CommandState.Executing;
    }

    private void BuildTower() {
        SetBuilderOrientationOnBuild();
        
        ServerTower t = EntityCreationEngine.CreateTower(
            Type,
            BuildLocation,
            Builder.ActiveLane,
            GoldCost
        );

        ServerSideGridSystem.Singleton.SetCellsOccupiedByTower(Cells, t);

        NavMeshSystem.Singleton.Rebake();

        State = CommandState.Finished;
    }

    private void SetBuilderOrientationOnBuild() {
        Builder.Navigation.Stop();

        Vector3 direction = (BuildLocation - Builder.transform.position).normalized;
        Builder.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Update() { }

    public void CleanUp() {
        Builder.Navigation.Stop();
        
        if (Trigger != null) {
            Object.Destroy(Trigger.gameObject);
        }
        
        // If the build order was cancelled before the tower could be built, refund the gold.
        if (State == CommandState.Executing) {
            Builder.ActiveLane.AddGold(GoldCost);
        }
    }

    private void CheckForCellBlockage() {
        foreach (MazeGridCell cell in Cells) {
            if (cell.Occupied) {
                State = CommandState.Failed_ShouldBeSkipped;
                
                ServerSend.DisplayChatMessageToClientWithID(
                    "There is another tower blocking you from building there!",
                    true,
                    RequestingClientID
                );

                break;
            }
        }
    }

    private void CheckForSufficientGold() {
        if (Builder.ActiveLane.Gold < GoldCost) {
            State = CommandState.Failed_ShouldBeSkipped;
            
            ServerSend.DisplayChatMessageToClientWithID(
                "You don't have enough gold to build that tower!",
                true,
                RequestingClientID
            );
        }
    }

    private void CheckForMazeBlockage() {
        if (
            !IsTechnologyDiscType(Type)
            && !NavMeshSystem.Singleton.IsNonBlockingBuildLocationForLane(
                BuildLocation,
                Builder.ActiveLane
            )
        ) {
            State = CommandState.Failed_ShouldBeSkipped;
            
            ServerSend.DisplayChatMessageToClientWithID(
                "Building that tower would block your maze!",
                true,
                RequestingClientID
            );
        }
    }

    private static bool IsTechnologyDiscType(TowerType type) {
        return 
            type >= TowerType.TechnologyDisc 
            && type <= TowerType.TechnologyDisc_Void_Ultimate;
    }
}