using UnityEngine;
using System;

[CreateAssetMenu]
public class SocketManager : ScriptableObject {
    public APIConfiguration APIConfiguration;
    BestHTTP.SocketIO.SocketManager manager;

    public BestHTTP.SocketIO.Socket Socket {
        get {
            if(manager != null) {
                return manager.Socket;
            }
            else {
                return null;
            }
        }
    }

    void OnEnable() {
        Connect();
    }

    void OnDisable() {
        if(manager != null) {
            manager.Close();
        }
    }

    public void Connect() {
        BestHTTP.SocketIO.SocketOptions options = new BestHTTP.SocketIO.SocketOptions();
        options.ConnectWith = BestHTTP.SocketIO.Transports.TransportTypes.WebSocket;
        manager = new BestHTTP.SocketIO.SocketManager(new Uri(APIConfiguration.HostURL + APIConfiguration.SocketIORoute), options);
        manager.Socket.On(BestHTTP.SocketIO.SocketIOEventTypes.Connect, OnConnected);
    }

    void OnConnected(BestHTTP.SocketIO.Socket socket, BestHTTP.SocketIO.Packet packet, object[] args) {
        Debug.Log("Connected to socket server");
    }
}