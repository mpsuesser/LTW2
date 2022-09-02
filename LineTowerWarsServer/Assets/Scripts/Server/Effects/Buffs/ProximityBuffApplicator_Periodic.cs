using System.Collections.Generic;
using UnityEngine;

public abstract class ProximityBuffApplicator_Periodic : ProximityBuffApplicator {
    private Dictionary<ServerEntity, float> TimeOfLastApplication { get; set; }
    
    // TODO: Set the periodic interval
    
    protected override void Awake() {
        base.Awake();

        TimeOfLastApplication = new Dictionary<ServerEntity, float>();
    }
    
    protected override void ApplyBuffToEntity(ServerEntity entity) {
        TimeOfLastApplication[entity] = Time.time;
        entity.OnDestroyed += RemoveAppliedBuffFromEntity;
    }

    protected override void RemoveAppliedBuffFromEntity(ServerEntity entity) {
        TimeOfLastApplication.Remove(entity);
    }

    protected virtual void Update() {
        foreach (ServerEntity entity in TimeOfLastApplication.Keys) {
            // TODO:
            // If time is up, apply buff (new instance or +1 stack) then reset time
        }
    }
    
    // TODO: Inherit from this abstraction for TorrentSlow1/2 and CripplingDecaySlow applicators
    // TODO: Instantiate those applicators from the traits, then consider them done
}