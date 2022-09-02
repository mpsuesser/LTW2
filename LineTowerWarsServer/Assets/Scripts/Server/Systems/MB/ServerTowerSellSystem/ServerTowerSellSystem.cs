using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTowerSellSystem : SingletonBehaviour<ServerTowerSellSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        ServerEventBus.OnRequestTowerSale += ProcessTowerSaleRequest;
        ServerEventBus.OnRequestTowerSaleCancellation += ProcessTowerSaleCancellationRequest;
    }

    private static void ProcessTowerSaleRequest(HashSet<ServerTower> towers) {
        foreach (ServerTower t in towers) {
            t.Sell.Begin();
        }
    }

    private static void ProcessTowerSaleCancellationRequest(HashSet<ServerTower> towers) {
        foreach (ServerTower t in towers) {
            t.Sell.Cancel();
        }
    }
}
