using UnityEngine;

public abstract class SettingsInterfaceContent : MonoBehaviour {
    [SerializeField] private GameObject content;

    protected abstract void LoadSettings();

    private void Start() {
        Invoke(nameof(LoadSettings), 0.1f);
    }

    private void Enable() {
        AdjustContentPosition();
    }

    private void AdjustContentPosition() {
        // TODO: Would be nice to adjust so that windows with few options aren't centered but rather build downward from the top
    }
}