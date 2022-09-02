public class BVoidLashingBuff1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.VoidLashingBuff1;

    protected override double BaseDuration => TraitConstants.VoidLashing1Duration;
    
    private float HealthRatioOfTargetUponApplication { get; }
    
    public BVoidLashingBuff1(
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
            * TraitConstants.VoidLashing1DamageMultiplierIncreaseByPercentageOfPrimaryTargetCurrentHealth;
}