public class BFrostAttackSlow1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.FrostAttackSlow1;

    protected override int MaxStackCount => TraitConstants.FrostAttackChill1MaxStacks;
    
    public BFrostAttackSlow1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 - TraitConstants.FrostAttackChill1MovementSpeedReductionMultiplierPerStack * Stacks;
}