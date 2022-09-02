using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewLobbiesInterface : InterfaceState, IHandleKeyDownInput {
    public override bool ShouldShortCircuitBeforeCheckingBaseInterfaces =>
        false;
    
    public override string Title => "View Lobbies";
    
    [SerializeField] private Button BackButton;
    
    private void Awake() {
        BackButton.onClick.AddListener(BackButtonPressed);
    }
    
    private static readonly HashSet<KeyCode> keyDownSubscriptions =
        new HashSet<KeyCode>() {
            KeyCode.Escape,
        };
    public HashSet<KeyCode> KeyDownSubscriptions => keyDownSubscriptions;
    public bool HandleInputKeyDown(KeyCode kc) {
        return kc switch {
            KeyCode.Escape => BackPressed(),
            _ => false
        };
    }

    private void BackButtonPressed() => BackPressed();
    private bool BackPressed() => Close();
}