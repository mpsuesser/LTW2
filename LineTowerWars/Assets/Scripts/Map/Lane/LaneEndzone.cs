using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneEndzone : MonoBehaviour
{
    private Collider C { get; set; }

    private void Awake() {
        C = GetComponent<Collider>();
    }

    public Vector3 GetClosestDestinationTo(Vector3 location) {
        return C.ClosestPoint(location);
    }
}