using System;
using Unity.Mathematics;
using UnityEngine;

public class TowerBuildLocationTrigger : MonoBehaviour {
    public static TowerBuildLocationTrigger Create(
        ServerBuilder builder,
        Vector3 location
    ) {
        TowerBuildLocationTrigger trigger = Instantiate(
            ServerPrefabs.Singleton.pfTowerBuildLocationTrigger,
            location,
            Quaternion.identity
        );

        trigger.Builder = builder;

        return trigger;
    }

    public event Action OnBuilderReachedTowerBuildLocation;
    
    private ServerBuilder Builder { get; set; }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject != Builder.gameObject) return;

        OnBuilderReachedTowerBuildLocation?.Invoke();
    }
}