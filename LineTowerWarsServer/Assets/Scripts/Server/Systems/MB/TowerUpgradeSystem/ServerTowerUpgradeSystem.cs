using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTowerUpgradeSystem : SingletonBehaviour<ServerTowerUpgradeSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        ServerEventBus.OnRequestTowerUpgrade += ProcessTowerUpgradeRequest;
        ServerEventBus.OnRequestTowerUpgradeCancellation += ProcessTowerUpgradeCancellationRequest;
    }

    private static void ProcessTowerUpgradeRequest(
        HashSet<ServerTower> towers,
        TowerUpgrade upgrade
    ) {
        foreach (ServerTower t in towers) {
            Lane lane = t.ActiveLane;
            if (lane.Gold < upgrade.Cost) {
                return;
            }

            if (upgrade.RequiredTech.Length > 0) {
                foreach (ElementalTechType tech in upgrade.RequiredTech) {
                    if (!lane.HasTech(tech)) {
                        return;
                    }
                }
            }

            t.Upgrade.Begin(upgrade);
        }
    }

    private static void ProcessTowerUpgradeCancellationRequest(HashSet<ServerTower> towers) {
        foreach (ServerTower t in towers) {
            t.Upgrade.Cancel();
        }
    }
}
