using UnityEngine;

public class SendMenuTavern : MonoBehaviour {
    public SendUnitOption[] CreepSendOptions => sendOptions;
    
    [SerializeField] private SendUnitOption[] sendOptions;
}