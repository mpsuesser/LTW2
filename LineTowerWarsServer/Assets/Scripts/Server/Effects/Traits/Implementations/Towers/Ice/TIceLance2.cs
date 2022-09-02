using System.Collections.Generic;
using UnityEngine;

public class TIceLance2 : Trait {
    public override TraitType Type => TraitType.IceLance2;

    public TIceLance2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPostIncludeSplashTargets += ApplyDebuffsToTargets;
    }

    private void ApplyDebuffsToTargets(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        foreach (ServerEntity target in targets) {
            BuffFactory.ApplyBuff(
                BuffType.IceLanced2,
                target,
                E
            );
        }        
    }

    public override bool HasCustomHandleAttackLandedImplementation => true;
    public override void CustomHandleAttackLandedImplementation(
        ServerEntity target,
        AttackEventData eventData,
        ref double damageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) {
        Vector3 direction =
            (target.transform.position - E.transform.position).normalized;
        
        // Get all units in line from E to target
        RaycastHit[] hits = Physics.BoxCastAll(
            E.transform.position,
            Vector3.one,
            direction,
            Quaternion.identity,
            ServerUtil.ConvertGameRangeToUnityRange(
                TraitConstants.IceLance2Range
            ),
            LayerMaskConstants.EnemyLayerMask
        );

        
        foreach (RaycastHit hit in hits) {
            ServerEnemy creep = hit.collider.gameObject.GetComponent<ServerEnemy>();
            if (
                creep == null
                || !creep.IsAlive
                || !E.Effects.AggregateAttackTargetEligibilityFilter.PassesFilter(creep)
            ) {
                continue;
            }
            
            allTargetsAccumulator.Add(creep);
            damageAccumulator += E.DealDamageTo(
                creep,
                eventData.InitialSnapshotDamage,
                eventData.DmgType,
                creep == target
                    ? DamageSourceType.AutoAttack
                    : DamageSourceType.AutoAttackSplash
            );
        }
    }
}