public class BCripplingDecaySlow : Buff_Static_Stacks {
    public override BuffType Type => BuffType.CripplingDecaySlow;

    protected override int MaxStackCount =>
        TraitConstants.CripplingDecayMaxStacks;
    
    public BCripplingDecaySlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 -
        TraitConstants.CripplingDecayMovementSpeedReductionPerStack * Stacks;
}