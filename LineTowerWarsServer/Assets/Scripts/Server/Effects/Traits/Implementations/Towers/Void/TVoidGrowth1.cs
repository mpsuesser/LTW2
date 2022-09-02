using System.Collections.Generic;
using UnityEngine;

public class TVoidGrowth1 : Trait {
    public override TraitType Type => TraitType.VoidGrowth1;

    public TVoidGrowth1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }
        
        tower.Attack.OnAttackFiredPre += CheckForGrowthOpportunity;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.VoidGrowth1ManaRegenPerSecond;
   
    private void CheckForGrowthOpportunity(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        if (!(attacker is ServerTower attackingTower)) {
            return;
        }
        
        if (attacker.MP < attacker.MaxMana) {
            return;
        }
        

        // Prefer certain tower types
        HashSet<ServerEntity> spreadCandidates =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                attacker,
                TraitConstants.VoidGrowth1Radius,
                new TowerEntityFilter(
                    TraitConstants.VoidGrowth1PreferredTowerTypes
                )
            );
        
        // If no preferred tower types in range, expand candidate pool to all eligible
        if (spreadCandidates.Count < 1) {
            spreadCandidates =
                TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                    attacker,
                    TraitConstants.VoidGrowth1Radius,
                    new TowerEntityFilter(
                        TraitConstants.VoidGrowth1EligibleTowerTypes
                    )
                );
        }

        if (spreadCandidates.Count < 1) {
            return;
        }

        ServerTower closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (ServerEntity e in spreadCandidates) {
            if (!(e is ServerTower tower)) {
                LTWLogger.LogError("Spread candidate was not a tower!");
                continue;
            }

            if (tower.ActiveLane != attackingTower.ActiveLane) {
                continue;
            }

            float distance = Vector3.Distance(
                E.transform.position,
                tower.transform.position
            );
            if (distance < closestDistance) {
                closestDistance = distance;
                closest = tower;
            }
        }

        if (closest == null) {
            return;
        }

        ServerTower convertedTower = EntityCreationEngine.CreateTower(
            TraitConstants.VoidGrowth1ResultingTowerType,
            closest.transform.position,
            closest.ActiveLane,
            closest.GoldValue + TowerConstants.BuildCost[TowerType.ElementalCore],
            closest
        );
        ServerSend.TowerUpgradeFinished(closest, convertedTower);

        attackingTower.Attack.OnAttackFiredPre -= CheckForGrowthOpportunity;
    }
}