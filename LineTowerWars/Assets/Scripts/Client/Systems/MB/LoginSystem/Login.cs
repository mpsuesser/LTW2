public class Login {
    public string PlayfabID { get; }
    public string Username { get; }
    public string AuthToken { get; }

    public Login(string playfabID, string username, string authToken) {
        PlayfabID = playfabID;
        Username = username;
        AuthToken = authToken;
    }
}
