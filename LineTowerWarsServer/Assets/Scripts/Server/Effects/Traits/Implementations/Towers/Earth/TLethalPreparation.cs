using System;
using System.Collections.Generic;

public class TLethalPreparation : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.LethalPreparation;
    
    private int TicksSinceLastAttack { get; set; }
    private System.Random RNG { get; set; }

    public TLethalPreparation(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        RNG = new Random();
        
        TicksSinceLastAttack = 0;
        Ticker.Subscribe(this);

        tower.Attack.OnAttackFiredPre += ResetBuffApplicationInterval;
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() => TraitConstants.LethalPreparationIdleStackIntervalInSeconds;
    public void DoPeriodicThing() {
        if (++TicksSinceLastAttack >= 2) {
            BuffFactory.ApplyBuff(
                BuffType.LethalPreparation,
                E,
                E
            );
        }
    }

    private void ResetBuffApplicationInterval(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        Ticker.RefreshSubscription(this);
        TicksSinceLastAttack = 0;
    }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        if (
            E.Buffs.HasBuffOfType(BuffType.LethalPreparation)
            || RNG.NextDouble() > target.HealthRatio
        ) {
            MakeCriticalStrike(ref damageAmount);
        }
    }

    private static void MakeCriticalStrike(ref double damageAmount) {
        damageAmount *= TraitConstants.OpportunisticCriticalStrikeDamageMultiplier;
    }
}