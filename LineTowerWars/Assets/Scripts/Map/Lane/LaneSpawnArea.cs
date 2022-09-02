using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawnArea : MonoBehaviour
{
    private Collider AreaCollider { get; set; }

    private void Awake() {
        AreaCollider = GetComponent<Collider>();
    }

    public Vector3 GetSpawnLocation() {
        return new Vector3(
            Random.Range(AreaCollider.bounds.min.x, AreaCollider.bounds.max.x),
            transform.position.y,
            Random.Range(AreaCollider.bounds.min.z, AreaCollider.bounds.max.z)
        );
    }
}