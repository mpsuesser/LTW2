public class BFrostBlastSlow2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.FrostBlastSlow2;

    protected override int MaxStackCount => TraitConstants.FrostBlastChill2MaxStacks;
    
    public BFrostBlastSlow2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 - TraitConstants.FrostBlastChill2MovementSpeedReductionMultiplierPerStack * Stacks;
}