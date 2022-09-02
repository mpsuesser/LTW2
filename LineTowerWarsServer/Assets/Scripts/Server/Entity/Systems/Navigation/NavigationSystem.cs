using UnityEngine;
using UnityEngine.AI;

public class NavigationSystem : IEntitySystem {
    protected const int MAX_PATH_CALCULATIONS_PER_FRAME = 10;
    public static int NumPathsCalculatedThisFrame = 0;

    public Vector3 CurrentPosition => E.transform.position;

    protected ServerEntity E { get; }

    protected double BaseMoveSpeed { get; }

    protected NavMeshAgent NMA { get; }

    protected bool DestinationSet { get; set; }

    public NavigationSystem(
        ServerEntity e,
        double baseMoveSpeed
    ) {
        E = e;
        
        NMA = E.GetComponent<NavMeshAgent>();
        DestinationSet = false;
        
        BaseMoveSpeed = baseMoveSpeed / GameConstants.MoveSpeedDivisor;
        NMA.speed = (float) BaseMoveSpeed;
    }
    
    public virtual void Update() { }

    public void SetDestination(Vector3 destination) {
        NMA.SetDestination(NormalizeDestination(destination));
        DestinationSet = true;
    }

    public void UpdatePositionTo(Vector3 location) {
        NMA.Warp(NormalizeDestination(location));
        DestinationSet = false;
    }

    public void Stop() {
        NMA.ResetPath();
        DestinationSet = false;
    }

    public void StopWithoutUnsettingDestinationFlag() {
        NMA.ResetPath();
    }

    private const float StoppingDistance = 2f;
    public bool HasReachedDestination(Vector3 dest) {
        return Vector3.Distance(
            E.transform.position,
            dest
        ) < StoppingDistance;
    }

    public bool HasReachedInternalDestination() => HasReachedDestination(NMA.destination);

    public bool HasPath() {
        return NMA.hasPath;
    }

    public Vector3 NormalizeDestination(Vector3 prevDest) {
        Vector3 destWithClampedXZ = ClampPointXZWithinBoundsOfGameObject(
            prevDest,
            E.ActiveLane.gameObject
        );
        
        return new Vector3(
            destWithClampedXZ.x,
            E.transform.position.y,
            destWithClampedXZ.z
        );
    }

    // This is currently only used for the lane bounds. Calculating the lane bounds
    // every frame for multiple units is super inefficient, so we should calculate this
    // just once and store it somewhere for reference instead.
    // TODO
    private static Vector3 ClampPointXZWithinBoundsOfGameObject(
        Vector3 point,
        GameObject obj
    ) {
        Bounds bounds = ServerUtil.GetBounds(obj);

        return new Vector3(
            Mathf.Clamp(point.x, bounds.min.x, bounds.max.x),
            point.y,
            Mathf.Clamp(point.z, bounds.min.z, bounds.max.z)
        );
    }
}
