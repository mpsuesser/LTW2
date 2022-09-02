using System;

public class ServerTowerEventSystem : SingletonBehaviour<ServerTowerEventSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        ServerEventBus.OnTowerUpgradeStarted += TowerUpgradeStarted;
        ServerEventBus.OnTowerUpgradeCanceled += TowerUpgradeCanceled;
        ServerEventBus.OnTowerUpgradeFinished += TowerUpgradeFinished;
        ServerEventBus.OnTowerSaleStarted += TowerSaleStarted;
        ServerEventBus.OnTowerSaleCanceled += TowerSaleCanceled;
        ServerEventBus.OnTowerSaleFinished += TowerSaleFinished;
    }

    private static void TowerSaleStarted(ServerTower tower) {
        ServerSend.TowerSaleStarted(tower);
    }

    private static void TowerSaleCanceled(ServerTower tower) {
        ServerSend.TowerSaleCancelled(tower);
    }

    private static void TowerSaleFinished(ServerTower tower) {
        ServerSend.TowerSaleFinished(tower);

        tower.ActiveLane.AddGold((int)Math.Round(tower.GoldValue * TowerConstants.SellReturnValue));

        ServerSideGridSystem.Singleton.FreeCellsOccupiedByTower(tower);

        Destroy(tower.gameObject);
    }

    private static void TowerUpgradeStarted(ServerTower tower, TowerUpgrade upgrade) {
        ServerSend.TowerUpgradeStarted(tower, upgrade);

        tower.ActiveLane.DeductGold(upgrade.Cost);
    }

    private static void TowerUpgradeCanceled(ServerTower tower, TowerUpgrade upgrade) {
        ServerSend.TowerUpgradeCancelled(tower);

        tower.ActiveLane.AddGold(upgrade.Cost);
    }

    private static void TowerUpgradeFinished(ServerTower oldTower, TowerUpgrade upgrade) {
        ServerTower newTower = EntityCreationEngine.CreateTower(
            upgrade.TargetTowerType,
            oldTower.transform.position,
            oldTower.ActiveLane, 
            oldTower.GoldValue + upgrade.Cost,
            oldTower
        );
        
        ServerSend.TowerUpgradeFinished(oldTower, newTower);
    }
}
