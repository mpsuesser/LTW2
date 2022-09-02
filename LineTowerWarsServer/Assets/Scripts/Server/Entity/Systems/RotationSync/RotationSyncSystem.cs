using UnityEngine;

public class RotationSyncSystem : IEntitySystem {
    private ServerTower T { get; set; }

    private Quaternion prevRotation;

    public RotationSyncSystem(ServerTower t) {
        T = t;

        prevRotation = Quaternion.identity;
    }

    public void Update() {
        if (prevRotation != T.transform.rotation) {
            ServerSend.TowerRotationSync(T);
            prevRotation = T.transform.rotation;
        }
    }
}