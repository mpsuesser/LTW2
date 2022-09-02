using UnityEngine;

public class BEarthShield : Buff_DurationBased_NoStacks, IDoesThingsPeriodically {
    public override BuffType Type => BuffType.EarthShield;

    protected override double BaseDuration => TraitConstants.EarthShieldDuration;
    
    public BEarthShield(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        // Remove chill/slow effects upon application
        foreach (Buff b in affectedEntity.Buffs.ActiveBuffs) {
            if (b.MovementSpeedMultiplier < .99f) {
                b.Purge();
            }
        }

        Ticker.Subscribe(this);
    }
    
    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }
    public double GetInterval() => TraitConstants.EarthShieldHealInterval;
    public void DoPeriodicThing() {
        if (AffectedEntity == null) {
            return;
        }

        AffectedEntity.Status.HealForAmount(
            TraitConstants.EarthShieldHealPercentageOfMaxHealth
            * AffectedEntity.MaxHealth
        );
    }
}