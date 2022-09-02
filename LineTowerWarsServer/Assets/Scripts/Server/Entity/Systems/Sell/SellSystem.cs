using UnityEngine;
using System;

public class SellSystem : IEntitySystem {
    private ServerTower T { get; }

    public bool InProgress { get; private set; }
    
    private float sellStartTime;
    private double sellDuration;

    public SellSystem(ServerTower t) {
        T = t;

        InProgress = false;
    }

    public void Update() {
        if (!InProgress) {
            return;
        }

        if (Time.time - sellStartTime > sellDuration) {
            Finish();
        }
    }

    public void Begin() {
        if (InProgress) return;
        
        sellStartTime = Time.time;
        sellDuration = TowerConstants.SellDuration;
        InProgress = true;

        ServerEventBus.TowerSaleStarted(T);
    }

    public void Cancel() {
        if (!InProgress) {
            return;
        }

        InProgress = false;

        ServerEventBus.TowerSaleCanceled(T);
    }

    private void Finish() {
        InProgress = false;

        ServerEventBus.TowerSaleFinished(T);
    }
}
