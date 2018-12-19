using UnityEngine;

public class SocketConnector : MonoBehaviour {
    public SocketManager SocketManager;

    void Start() {
        SocketManager.Connect();
    }
}