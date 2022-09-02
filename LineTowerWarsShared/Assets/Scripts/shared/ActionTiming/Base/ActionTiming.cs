using UnityEngine;

public abstract class ActionTiming : ScriptableObject {
    [SerializeField] public TowerType SourceTowerType;

    [Space(5)]
    [SerializeField] public double InitialAnimationDelay;
}