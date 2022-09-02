using UnityEngine;
using System;

public class DiscStepTrigger : MonoBehaviour {
    public static DiscStepTrigger Create(ServerEntity disc) {
        DiscStepTrigger trigger = Instantiate(
            ServerPrefabs.Singleton.pfDiscStepTrigger,
            disc.transform.position,
            Quaternion.identity
        );

        BoxCollider collider = trigger.GetComponent<BoxCollider>();
        collider.size = Vector3.one * 10;

        disc.OnDestroyed += trigger.DiscDestroyed;

        return trigger;
    }

    public event Action<ServerEnemy> OnStepTriggerActivated;

    private void OnTriggerEnter(Collider other) {
        ServerEnemy creep = other.gameObject.GetComponent<ServerEnemy>();
        if (creep == null) {
            return;
        }
        
        OnStepTriggerActivated?.Invoke(creep);
    }

    private void DiscDestroyed(ServerEntity _disc) {
        Destroy(gameObject);
    }
}