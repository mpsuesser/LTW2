public abstract class Buff_Static_NoStacks : Buff {
    protected override BuffExpirationType ExpirationType
        => BuffExpirationType.Static;

    protected override bool IsStacking => false;
    
    protected Buff_Static_NoStacks(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity,
        1
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
    ) {}
}