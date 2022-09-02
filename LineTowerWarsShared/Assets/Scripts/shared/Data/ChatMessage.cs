public class ChatMessage {
    public int ID { get; }
    public PlayerInfo Sender { get; }
    public string Content { get; }

    public ChatMessage(int id, PlayerInfo sender, string content) {
        ID = id;
        Sender = sender;
        Content = content;
    }
}