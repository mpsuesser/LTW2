public class BFrostBlastSlow1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.FrostBlastSlow1;

    protected override int MaxStackCount => TraitConstants.FrostBlastChill1MaxStacks;
    
    public BFrostBlastSlow1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 - TraitConstants.FrostBlastChill1MovementSpeedReductionMultiplierPerStack * Stacks;
}