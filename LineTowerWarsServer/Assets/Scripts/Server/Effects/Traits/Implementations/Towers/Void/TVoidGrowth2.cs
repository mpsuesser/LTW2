using System.Collections.Generic;
using UnityEngine;

public class TVoidGrowth2 : Trait {
    public override TraitType Type => TraitType.VoidGrowth2;

    public TVoidGrowth2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }
        
        tower.Attack.OnAttackFiredPre += CheckForGrowthOpportunity;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.VoidGrowth2ManaRegenPerSecond;
   
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

        HashSet<ServerEntity> spreadCandidates =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                attacker,
                TraitConstants.VoidGrowth2Radius,
                new TowerEntityFilter(
                    TraitConstants.VoidGrowth2EligibleTowerTypes
                )
            );
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

        // Get the upgrade cost to add to the current gold value of the existing tower
        int upgradeValue;
        try {
            TowerUpgrade upgrade =
                TowerUpgrades.Singleton.GetUpgradeBySourceAndTargetTowerType(
                    closest.Type,
                    TraitConstants.VoidGrowth2ResultingTowerType
                );
            upgradeValue = upgrade.Cost;
        }
        catch (ResourceNotFoundException) {
            upgradeValue = 800; // just default it if we've done something wrong
        }

        ServerTower convertedTower = EntityCreationEngine.CreateTower(
            TraitConstants.VoidGrowth2ResultingTowerType,
            closest.transform.position,
            closest.ActiveLane,
            closest.GoldValue + upgradeValue,
            closest
        );
        ServerSend.TowerUpgradeFinished(closest, convertedTower);

        attackingTower.Attack.OnAttackFiredPre -= CheckForGrowthOpportunity;
    }
}