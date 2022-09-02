using System.Collections.Generic;
using UnityEngine;

public static class TemporalRiftHistory {
    private static readonly Dictionary<
        /* entity ID */ int,
        /* time of application */ float
    > LastApplicationTime = new Dictionary<int, float>();

    public static float GetTimeOfLastApplicationForEntity(ServerEntity entity) {
        if (!LastApplicationTime.TryGetValue(entity.ID, out float last)) {
            return 0f;
        }

        return last;
    }

    public static void RegisterApplicationToEntity(ServerEntity entity) {
        LastApplicationTime[entity.ID] = Time.time;
    }
}