using UnityEngine;

public abstract class ActionTimingWithProjectile : ActionTiming {
    [SerializeField] public double ProjectileSpeed;

    [SerializeField] public bool TrackTarget;

    [SerializeField] public Vector3 ProjectileInitialOffset;

    [SerializeField] public double MaxDistance = 100;
    [SerializeField] public double MaxSeconds = 2;
}