using System.Collections.Generic;

public abstract class Buff_ProximityBased_NoStacks : Buff_ProximityBased {
    protected override bool IsStacking => false;
    
    private HashSet<ServerEntity> Applicators { get; }

    protected Buff_ProximityBased_NoStacks(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity,
        1
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
        Applicators.Remove(applicator);

        if (Applicators.Count == 0) {
            Remove();
        }
    }

    private void RegisterNewApplicator(ServerEntity applicator) {
        Applicators.Add(applicator);

        applicator.OnDestroyed += RemoveStackFrom;
    }
}