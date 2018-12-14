using UnityEngine;

public class SocketConnector : MonoBehaviour {
    public SocketManager SocketManager;

    void Start() {
        if(!SocketManager.IsConnected) {
            SocketManager.Connect();
        }
    }
}