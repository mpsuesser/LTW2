using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeSystem : MonoBehaviour
{
    private static readonly float IncomeInterval = GameConstants.IncomeInterval;
    private float timeSinceLastIncome;

    private void Awake() {
        timeSinceLastIncome = -10000f;
    }

    private void Start() {
        ServerEventBus.OnGameStarted += StartIncomeTimer;
    }

    private void StartIncomeTimer() {
        timeSinceLastIncome = 0f;
    }

    private void Update() {
        timeSinceLastIncome += Time.deltaTime;
        if (timeSinceLastIncome > IncomeInterval) {
            DeliverIncomes();
            timeSinceLastIncome = 0f;
        }
    }

    private void DeliverIncomes() {
        foreach (Lane lane in LaneSystem.Singleton.Lanes) {
            if (lane.IsActive) {
                lane.AddGold(lane.Income);
            }
        }

        ServerSend.IncomeDelivered(IncomeInterval);
    }
}
