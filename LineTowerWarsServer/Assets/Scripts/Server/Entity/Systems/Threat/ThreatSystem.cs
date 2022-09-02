using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSystem : IEntitySystem {
    public ServerEntity Target { get; private set; }
    public bool HasTarget => Target != null;

    private ServerEntity E { get; }
    public float GameRange { get; }
    private float UnityRange { get; }

    public ThreatSystem(ServerEntity e, float gameRange) {
        E = e;

        GameRange = gameRange;
        UnityRange = ServerUtil.ConvertGameRangeToUnityRange(GameRange);
    }

    public void SetTarget(ServerEntity e) {
        Target = e;
    }

    public void Update() {
        if (Target == null || !Target.IsAlive || !IsWithinRange(Target)) {
            SeekNewTarget();
        }
    }

    private void SeekNewTarget() {
        Target = null;

        HashSet<ServerEntity> targetsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                GameRange,
                E.Effects.AggregateAttackTargetEligibilityFilter
            );

        ServerEntity closest = null;
        ServerEntity closestNonSkittering = null;
        float closestDistance = Mathf.Infinity;
        
        foreach (ServerEntity potentialTarget in targetsInRange) {
            if (!potentialTarget.IsAlive) {
                continue;
            }

            float dist = Vector3.Distance(
                E.transform.position, 
                potentialTarget.transform.position
            );
            if (dist < closestDistance) {
                closest = potentialTarget;
                closestDistance = dist;
                if (!potentialTarget.AssociatedTraitTypes.Contains(TraitType.Skittering)) {
                    closestNonSkittering = potentialTarget;
                }
            }
        }

        if (closest != null) {
            SetTarget(closestNonSkittering != null ? closestNonSkittering : closest);
        }
    }

    private bool IsWithinRange(ServerEntity e) {
        return Vector3.Distance(
            E.transform.position, 
            e.transform.position
        ) < UnityRange;
    }
}
