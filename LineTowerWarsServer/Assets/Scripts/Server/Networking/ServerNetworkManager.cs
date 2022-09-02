using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;

public class ServerNetworkManager : SingletonBehaviour<ServerNetworkManager>
{
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    public Server Server { get; private set; }

    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

#if UNITY_EDITOR
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#endif

        Server = new Server();
        Server.ClientConnected += ServerReceive.NewPlayerConnected;
        Server.ClientDisconnected += ServerReceive.PlayerLeft;

        Server.Start(port, maxClientCount);
    }

    private void FixedUpdate() {
        Server.Tick();
    }

    private void OnApplicationQuit() {
        Server.Stop();

        Server.ClientConnected -= ServerReceive.NewPlayerConnected;
        Server.ClientDisconnected -= ServerReceive.PlayerLeft;
    }
}
