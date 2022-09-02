public abstract class Buff_DurationBased : Buff {
    protected abstract double BaseDuration { get; }

    protected double AdjustedBaseDuration {
        get {
            if (!IsDebuff || AffectedEntity == null) {
                return BaseDuration;
            }

            return BaseDuration * AffectedEntity.Effects.AggregateHarmfulEffectDurationMultiplier;
        }
    }

    protected override BuffExpirationType ExpirationType
        => BuffExpirationType.DurationBased;
    
    protected Buff_DurationBased(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount = 1
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) { }
}