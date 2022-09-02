using System;
using System.Collections.Generic;

public class TChaosPortal : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.ChaosPortal;
    
    private int CastCount { get; set; }

    public TChaosPortal(ServerEntity entity) : base(entity) {
        CastCount = 0;
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() =>
        TraitConstants.ChaosPortalCastIntervals[
            Math.Min(
                TraitConstants.ChaosPortalCastIntervals.Length,
                CastCount
            )
        ];
    public void DoPeriodicThing() {
        HashSet<ServerEntity> nearbyChaosWardens =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.ChaosPortalRange,
                new CreepEntityFilter(new HashSet<EnemyType> {EnemyType.ChaosWarden}),
                false
            );

        ServerEntity highestMPWarden = null;
        int highestMP = -1;
        foreach (ServerEntity warden in nearbyChaosWardens) {
            if (warden.MP > highestMP) {
                highestMPWarden = warden;
                highestMP = warden.MP;
            }
        }

        if (highestMPWarden != null && E is ServerEnemy e) {
            e.Navigation.UpdatePositionTo(highestMPWarden.transform.position);
        }

        CastCount++;
    }
}