using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using System;

public class ClientNetworkManager : SingletonBehaviour<ClientNetworkManager> {

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    public Client Client { get; private set; }

    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;
    }

    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();

        Client.Connected -= DidConnect;
        Client.ConnectionFailed -= FailedToConnect;
        Client.ClientDisconnected -= PlayerLeft;
        Client.Disconnected -= DidDisconnect;
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
    }

    public void Disconnect() {
        Client.Disconnect();
        
        EventBus.DisconnectedFromLobby();
    }

    private static void DidConnect(object sender, EventArgs e) {
        EventBus.ConnectedToLobby();
    }

    private static void FailedToConnect(object sender, EventArgs e)
    {
        // TODO: Do a thing
    }

    private static void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        // TODO: Do a thing
    }

    private static void DidDisconnect(object sender, EventArgs e) {
        LTWLogger.Log("Did disconnect called");
        
        EventBus.DisconnectedFromLobby();
    }
}
