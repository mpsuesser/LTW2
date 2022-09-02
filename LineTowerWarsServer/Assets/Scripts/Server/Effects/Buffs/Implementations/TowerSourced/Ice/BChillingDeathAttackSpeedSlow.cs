public class BChillingDeathAttackSpeedSlow : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.ChillingDeathAttackSpeedSlow;

    public BChillingDeathAttackSpeedSlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier => TraitConstants.ChillingDeathAttackSpeedMultiplier;
}