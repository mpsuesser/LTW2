using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NavMeshSystem : SingletonBehaviour<NavMeshSystem> {
    [SerializeField] private NavMeshSurface actualSurface;
    [SerializeField] private NavMeshSurface phantomSurface;
    [SerializeField] private NavMeshSurface flyingUnitSurface;

    private Dictionary<Lane, NavMeshInvisiblePathTester> PathTestersByLane { get; set; }

    private void Awake() {
        InitializeSingleton(this);

        PathTestersByLane = new Dictionary<Lane, NavMeshInvisiblePathTester>();

        LaneEventBus.OnLaneCreated += HandleNewLaneCreation;

        ServerEventBus.OnTowerSaleFinished += RebakeAfterTowerRemoved;
    }

    private void OnDestroy() {
        LaneEventBus.OnLaneCreated -= HandleNewLaneCreation;

        ServerEventBus.OnTowerSaleFinished -= RebakeAfterTowerRemoved;
    }

    private void Start() {
        Rebake();
        RebakePhantomMesh();
    }

    private void RebakeAfterTowerRemoved(ServerTower tower) {
        RebakeForLane(tower.ActiveLane);
    }

    private void HandleNewLaneCreation(Lane lane) {
        RebakeForLane(lane);
        RebakePhantomMeshForLane(lane);
        RebakeFlyingUnitMeshForLane(lane);
        
        NavMeshInvisiblePathTester tester = NavMeshInvisiblePathTester.CreateInLane(lane);
        
        PathTestersByLane.Add(lane, tester);
    }

    // TODO: Consider making this lane-specific. Maybe make the entire system lane-specific
    public void Rebake() {
        actualSurface.BuildNavMesh();
    }

    private void RebakePhantomMesh() {
        phantomSurface.BuildNavMesh();
    }

    private void RebakeFlyingUnitMesh() {
        flyingUnitSurface.BuildNavMesh();
    }

    private void RebakeForLane(Lane lane) {
        // TODO
        Rebake();
    }

    private void RebakePhantomMeshForLane(Lane lane) {
        // TODO
        RebakePhantomMesh();
    }

    private void RebakeFlyingUnitMeshForLane(Lane lane) {
        // TODO
        RebakeFlyingUnitMesh();
    }

    public bool IsNonBlockingBuildLocationForLane(Vector3 buildLocation, Lane lane) {
        if (!PathTestersByLane.ContainsKey(lane)) {
            LTWLogger.Log($"Could not get path tester for lane ID {lane.ID}! Should not happen");
            return false;
        }

        NavMeshInvisiblePathTester pathTester = PathTestersByLane[lane];
        NavMeshInvisiblePathBlocker pathBlocker = Instantiate(
            ServerPrefabs.Singleton.pfNavMeshInvisiblePathBlocker,
            buildLocation,
            Quaternion.identity
        );
        
        RebakePhantomMeshForLane(lane);
        Destroy(pathBlocker.gameObject);

        return pathTester.IsAbleToGeneratePhantomPathToEndOfLane();
    }
}
