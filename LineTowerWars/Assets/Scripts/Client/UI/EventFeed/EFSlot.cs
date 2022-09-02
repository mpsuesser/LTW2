using UnityEngine;

public class EFSlot : MonoBehaviour {
    public EFEvent LoadedEvent { get; set; }
    public bool IsEmpty => LoadedEvent == null;

    private void Awake() {
        LoadedEvent = null;
    }
    
    public void LoadEvent(EFEvent efEvent) {
        if (!IsEmpty) {
            Clear();
        }

        switch (efEvent) {
            case EFEvent_LivesExchanged leEvent:
                EFEventDisplay_LivesExchanged.Create(leEvent, transform);
                break;
            
            default:
                LTWLogger.Log($"Could not create a display for EFEvent type {efEvent.Type} because it was not yet implemented!");
                return;
        }

        LoadedEvent = efEvent;
    }

    public void Clear() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        LoadedEvent = null;
    }
}