public abstract class Buff_ProximityBased : Buff {
    protected override BuffExpirationType ExpirationType
        => BuffExpirationType.ProximityBased;
    
    protected Buff_ProximityBased(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount = 1
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) { }
    
    public override BuffTransitData PackageDataForTransit() {
        return new BuffTransitData(
            ID,
            Type,
            Stacks,
            false
        );
    }

    public abstract void RemoveStackFrom(ServerEntity applicator);
}