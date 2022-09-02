using System.Collections.Generic;
using UnityEngine;

public class MeteoricGroundBurner : MonoBehaviour, IDoesThingsPeriodically {
    public static MeteoricGroundBurner Create(
        Vector3 location,
        ServerEntity sourceEntity,
        float damagePerTick,
        DamageSourceType damageSource,
        int ticksBeforeDestroy,
        bool shouldApplyMeteoricVulnerability
    ) {
        MeteoricGroundBurner burner = Instantiate(
            ServerPrefabs.Singleton.pfMeteoricGroundBurner,
            location,
            Quaternion.identity
        );
        
        burner.SourceEntity = sourceEntity;
        burner.DamagePerTick = damagePerTick;
        burner.DamageSource = damageSource;
        burner.TicksBeforeDestroy = ticksBeforeDestroy;
        burner.ShouldApplyMeteoricVulnerability = shouldApplyMeteoricVulnerability;
        
        burner.Ticks = 0;
        
        Ticker.Subscribe(burner);

        return burner;
    }
    
    private ServerEntity SourceEntity { get; set; }
    private float DamagePerTick { get; set; }
    private DamageSourceType DamageSource { get; set; }
    private int TicksBeforeDestroy { get; set; }
    private bool ShouldApplyMeteoricVulnerability { get; set; }

    private int Ticks { get; set; }

    public double GetInterval() => 1;
    public void DoPeriodicThing() {
        HashSet<ServerEntity> creepsWithinRadius =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfObject(
                gameObject,
                TraitConstants.MeteoricGroundBurnRadius,
                new CreepEntityFilter()
            );

        foreach (ServerEntity creep in creepsWithinRadius) {
            if (SourceEntity != null) {
                SourceEntity.DealDamageTo(
                    creep,
                    DamagePerTick,
                    DamageType.Spell,
                    DamageSource
                );
            }
            else {
                creep.TakeDamageFrom(
                    null,
                    DamagePerTick,
                    DamageType.Spell,
                    DamageSource
                );
            }

            if (ShouldApplyMeteoricVulnerability) {
                BuffFactory.ApplyBuff(
                    BuffType.MeteoricVulnerability,
                    creep,
                    SourceEntity
                );
            }
        }

        if (++Ticks >= TicksBeforeDestroy) {
            Ticker.Unsubscribe(this);
            Destroy(gameObject);
        }
    }
}