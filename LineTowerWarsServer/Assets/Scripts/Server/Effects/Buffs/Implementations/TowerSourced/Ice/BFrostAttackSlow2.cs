public class BFrostAttackSlow2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.FrostAttackSlow2;

    protected override int MaxStackCount => TraitConstants.FrostAttackChill2MaxStacks;
    
    public BFrostAttackSlow2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 - TraitConstants.FrostAttackChill2MovementSpeedReductionMultiplierPerStack * Stacks;
}