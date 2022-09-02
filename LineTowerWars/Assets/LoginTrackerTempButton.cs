using UnityEngine;
using TMPro;

public class LoginTrackerTempButton : MonoBehaviour {
    private TMP_Text Text { get; set; }
    
    private void Awake() {
        Text = GetComponentInChildren<TMP_Text>(true);
        
        EventBus.OnLoginSuccess += UpdateText;
        EventBus.OnLoggedOut += UpdateText;
    }

    private void Start() {
        UpdateText();
    }

    private void OnDestroy() {
        EventBus.OnLoginSuccess -= UpdateText;
        EventBus.OnLoggedOut -= UpdateText;
    }

    private void UpdateText() {
        if (LoginSystem.Singleton.IsLoggedIn) {
            Text.SetText($"Log out as {LoginSystem.Singleton.ActiveUsername}");
        }
        else {
            Text.SetText("-");
        }
    }
}
