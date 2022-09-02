using UnityEngine;

public class MapPrefabs : SingletonBehaviour<MapPrefabs>
{
    [SerializeField] public Lane pfLane;

    private void Awake() {
        InitializeSingleton(this);
    }
}