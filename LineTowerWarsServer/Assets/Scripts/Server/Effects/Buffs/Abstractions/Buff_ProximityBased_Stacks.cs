using System.Collections.Generic;

public abstract class Buff_ProximityBased_Stacks : Buff_ProximityBased {
    protected override bool IsStacking => true;
    
    private HashSet<ServerEntity> Applicators { get; }

    protected Buff_ProximityBased_Stacks(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount = 1
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) {
        Applicators = new HashSet<ServerEntity>();
        
        RegisterNewApplicator(appliedByEntity);
    }

    public override void AddStacksFrom(
        int additionalStackCount,
        ServerEntity appliedByEntity
    ) {
        RegisterNewApplicator(appliedByEntity);
    }

    public override void RemoveStackFrom(ServerEntity applicator) {
        if (Applicators.Remove(applicator)) {
            RemoveStack();
        }

        if (Applicators.Count == 0) {
            Remove();
        }
    }

    private void RegisterNewApplicator(ServerEntity applicator) {
        if (Applicators.Add(applicator)) {
            AddStack();
        }

        applicator.OnDestroyed += RemoveStackFrom;
    }
}