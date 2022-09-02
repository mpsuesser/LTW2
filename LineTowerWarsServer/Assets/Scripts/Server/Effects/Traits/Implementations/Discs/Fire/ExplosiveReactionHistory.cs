using System.Collections.Generic;
using UnityEngine;

public static class ExplosiveReactionHistory {
    private static readonly Dictionary<
        /* entity ID */ int,
        /* time of reaction */ float
    > LastReactionTime = new Dictionary<int, float>();

    public static float GetTimeOfLastReactionForEntity(ServerEntity entity) {
        if (!LastReactionTime.TryGetValue(entity.ID, out float last)) {
            return 0f;
        }

        return last;
    }

    public static void RegisterReactionOnEntity(ServerEntity entity) {
        LastReactionTime[entity.ID] = Time.time;
    }
}