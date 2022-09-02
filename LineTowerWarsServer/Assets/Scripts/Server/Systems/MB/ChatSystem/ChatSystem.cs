using System.Collections.Generic;

public class ChatSystem : SingletonBehaviour<ChatSystem> { 
    private List<ChatMessage> Messages { get; set; }
    private void Awake() {
        InitializeSingleton(this);

        Messages = new List<ChatMessage>();

        ServerEventBus.OnNewChatMessageReceived += HandleNewMessage;
    }

    private void OnDestroy() {
        ServerEventBus.OnNewChatMessageReceived -= HandleNewMessage;
    }

    private void HandleNewMessage(ChatMessage message) {
        StoreNewMessage(message);
        ForwardNewMessage(message);
    }

    private void StoreNewMessage(ChatMessage message) {
        Messages.Add(message);
    }

    private void ForwardNewMessage(ChatMessage message) {
        ServerSend.ChatMessageReceived(message);
    }
}