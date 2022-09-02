using UnityEngine;

public class ChatWindow : MonoBehaviour {
    [SerializeField] private ChatEntryInterface entryInterface;
    [SerializeField] private ChatMessagesWindow messagesWindow;
    
    private void Awake() {
        entryInterface.OnOpened += HandleEntryInterfaceOpened;
        entryInterface.OnClosed += HandleEntryInterfaceClosed;
    }

    private void HandleEntryInterfaceOpened(InterfaceState entryState) {
        messagesWindow.SetMessageEntryOpen();
    }

    private void HandleEntryInterfaceClosed(InterfaceState entryState) {
        messagesWindow.SetMessageEntryClosed();
    }
}