using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To potentially be expanded in the future for different game modes
public class LaneSendMappingSystem : SingletonBehaviour<LaneSendMappingSystem>
{
    private LaneSystem LS { get; set; }

    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        LS = LaneSystem.Singleton;
    }

    public Lane GetLaneReceivingFrom(Lane sendingLane) {
        int sendingLaneID = LS.LaneIDByLane[sendingLane];

        int nextLaneSkip = 1;
        int maxLanes = LS.MaxLaneCount;
        while (nextLaneSkip <= maxLanes) {
            int nextLaneIndex = (sendingLaneID + nextLaneSkip) % maxLanes;
            if (LS.Lanes[nextLaneIndex].IsActive) {
                return LS.Lanes[nextLaneIndex];
            }
            nextLaneSkip++;
        }

        throw new LaneNotFoundException();
    }

    public Lane GetLaneSendingTo(Lane receivingLane) {
        int receivingLaneID = LS.LaneIDByLane[receivingLane];

        int nextLaneSkip = -1;
        int maxLanes = LS.MaxLaneCount;
        while (-nextLaneSkip <= maxLanes) {
            int nextLaneIndex = (receivingLaneID + nextLaneSkip) % maxLanes;
            if (LS.Lanes[nextLaneIndex].IsActive) {
                return LS.Lanes[nextLaneIndex];
            }
            nextLaneSkip--;
        }

        throw new LaneNotFoundException();
    }
}
