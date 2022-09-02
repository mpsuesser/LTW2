using UnityEngine;
using UnityEngine.AI;

public class NavMeshInvisiblePathTester : MonoBehaviour {
    public static NavMeshInvisiblePathTester CreateInLane(Lane lane) {
        Vector3 pos = lane.SpawnArea.transform.position;
        pos.y = 1;
        
        NavMeshInvisiblePathTester tester = Instantiate(
            ServerPrefabs.Singleton.pfNavMeshInvisiblePathTester,
            lane.SpawnArea.transform.position,
            Quaternion.identity
        );

        tester.SetLane(lane);

        return tester;
    }
    
    
    private NavMeshAgent NMA { get; set;  }
    private Lane MyLane { get; set; }

    private void Awake() {
        NMA = GetComponent<NavMeshAgent>();
        NMA.speed = 0f;
    }

    private void SetLane(Lane lane) {
        MyLane = lane;
    }

    public bool IsAbleToGeneratePhantomPathToEndOfLane() {
        NavMeshPath path = new NavMeshPath();
        NMA.CalculatePath(
            MyLane.Endzone.GetClosestDestinationTo(transform.position),
            path
        );

        // Status becomes NavMeshPathStatus.PathPartial when there is blockage
        return path.status == NavMeshPathStatus.PathComplete;
    }
}