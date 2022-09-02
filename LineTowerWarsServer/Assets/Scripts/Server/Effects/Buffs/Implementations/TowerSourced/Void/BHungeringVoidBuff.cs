public class BHungeringVoidBuff : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.HungeringVoidBuff;

    protected override double BaseDuration => TraitConstants.HungeringVoidBuffDuration;
    
    private float HealthRatioOfTargetUponApplication { get; }
    
    public BHungeringVoidBuff(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        HealthRatioOfTargetUponApplication = (float)appliedByEntity.HP / appliedByEntity.MaxHealth;
    }

    public override float DamageDoneMultiplier =>
        1 +
            HealthRatioOfTargetUponApplication
            * TraitConstants.HungeringVoidDamageDealtAndAttackSpeedMultiplierIncreaseByPercentageOfPrimaryTargetCurrentHealth;
    
    public override float AttackSpeedMultiplier =>
        1 +
            HealthRatioOfTargetUponApplication
            * TraitConstants.HungeringVoidDamageDealtAndAttackSpeedMultiplierIncreaseByPercentageOfPrimaryTargetCurrentHealth;
}