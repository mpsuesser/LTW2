public class BVoidLashingBuff2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.VoidLashingBuff2;

    protected override double BaseDuration => TraitConstants.VoidLashing2Duration;
    
    private float HealthRatioOfTargetUponApplication { get; }
    
    public BVoidLashingBuff2(
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
        * TraitConstants.VoidLashing2DamageMultiplierIncreaseByPercentageOfPrimaryTargetCurrentHealth;
}