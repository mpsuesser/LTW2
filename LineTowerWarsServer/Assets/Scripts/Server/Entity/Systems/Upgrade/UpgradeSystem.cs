using UnityEngine;

public class UpgradeSystem : IEntitySystem {
    private ServerTower T { get; }

    public bool InProgress { get; private set; }
    
    private float upgradeStartTime;
    private double upgradeDuration;
    private TowerUpgrade activeUpgrade;

    public UpgradeSystem(ServerTower t) {
        T = t;

        InProgress = false;
        activeUpgrade = null;
    }

    public void Update() {
        if (!InProgress) {
            return;
        }

        if (Time.time - upgradeStartTime > upgradeDuration) {
            Finish(activeUpgrade);
        }
    }

    public void Begin(TowerUpgrade upgrade) {
        if (InProgress) return;
        InProgress = true;

        activeUpgrade = upgrade;
        upgradeStartTime = Time.time;
        upgradeDuration = upgrade.Duration;

        ServerEventBus.TowerUpgradeStarted(T, upgrade);
    }

    public void Cancel() {
        if (InProgress == false || activeUpgrade == null) return;

        InProgress = false;
        activeUpgrade = null;

        ServerEventBus.TowerUpgradeCanceled(T, activeUpgrade);
    }

    private void Finish(TowerUpgrade upgrade) {
        InProgress = false;

        ServerEventBus.TowerUpgradeFinished(T, upgrade);
    }
}
