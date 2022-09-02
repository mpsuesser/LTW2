public abstract class Buff_Static_Stacks : Buff {
    protected override BuffExpirationType ExpirationType
        => BuffExpirationType.Static;

    protected override bool IsStacking => true;
    
    protected Buff_Static_Stacks(
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

    public override void AddStacksFrom(
        int additionalStackCount,
        ServerEntity appliedByEntity
    ) {
        AddStacks(additionalStackCount);
    }
}