using UnityEngine;

public class AppBootstrapper : MonoBehaviour {
    void Start() {
        GameManager.Instance.FetchGameAsync();
        ConfigManager.Instance.FetchConfigAsync();
        Clock.Instance.SyncToServerClock();
        
        if(!SocketManager.Instance.IsConnected) {
            SocketManager.Instance.Connect();
        }
    }
}