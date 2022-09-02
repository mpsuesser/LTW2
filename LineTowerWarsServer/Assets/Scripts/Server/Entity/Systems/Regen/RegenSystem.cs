using System;
using UnityEngine;

public class RegenSystem : IEntitySystem, IDoesThingsPeriodically
{
    private ServerEntity E { get; }
    
    private float BaseHealthRegenPerSecond { get; }
    private float BaseManaRegenPerSecond { get; }

    private float AdjustedHealthRegenPerSecond =>
        BaseHealthRegenPerSecond;
    private float AdjustedManaRegenPerSecond =>
        BaseManaRegenPerSecond + E.Effects.AggregateManaRegenPerSecondDiff;

    public RegenSystem(ServerEntity e) {
        E = e;

        BaseHealthRegenPerSecond = 0;
        BaseManaRegenPerSecond = 0; // TODO (maybe?)

        if (e is ServerEnemy creep) {
            BaseHealthRegenPerSecond = (float) EnemyConstants.RegenPerSecond[creep.Type];
        }

        Ticker.Subscribe(this);
    }

    private const double INTERVAL = .5;
    public double GetInterval() => INTERVAL;
    public void DoPeriodicThing() {
        if (E == null || !E.IsAlive) {
            Ticker.Unsubscribe(this);
            return;
        }
        
        E.Status.HealForAmount(AdjustedHealthRegenPerSecond * INTERVAL);
        E.Status.GainMana(AdjustedManaRegenPerSecond * INTERVAL);
    }
}