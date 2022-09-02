using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostgameInterface : InterfaceState {
    public override bool ShouldShortCircuitBeforeCheckingBaseInterfaces =>
        false;
    
    [SerializeField] private Button ReturnToMainMenuButton;
    
    private void Awake() {
        ReturnToMainMenuButton.onClick.AddListener(ReturnToMainMenuButtonPressed);
    }

    private static void ReturnToMainMenuButtonPressed() {
        ClientNetworkManager.Singleton.Disconnect();
    }
}
