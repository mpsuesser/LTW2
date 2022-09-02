using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInterface : MonoBehaviour
{
    [SerializeField] private TargetFrame Frame;
    [SerializeField] private TargetOptions Options;

    private void Start() {
        UpdateInterfaceVisibility(TargetSystem.Singleton.Targets);
        
        EventBus.OnTargetsUpdated += UpdateInterfaceVisibility;
    }

    private void OnDestroy() {
        EventBus.OnTargetsUpdated -= UpdateInterfaceVisibility;
    }

    private void UpdateInterfaceVisibility(List<ClientEntity> targets) {
        LTWLogger.Log("Updating target interface visibility...");
        if (targets.Count == 0) {
            LTWLogger.Log("No targets...");
            Frame.ClearAndHide();
            Options.ClearAndHide();
        } else {
            LTWLogger.Log("Show for targets...");
            Frame.ShowForTargets(targets);
            Options.ShowForTargets(targets);
        }
    }
}
