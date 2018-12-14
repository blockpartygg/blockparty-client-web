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

    public bool IsConnected;

    void OnEnable() {
        if(!IsConnected) {
            Connect();
        }
    }

    void OnDisable() {
        if(IsConnected) {
            if(manager != null) {
                manager.Close();
            }
            IsConnected = false;
        }  
    }

    public void Connect() {
        if(!IsConnected) {
            BestHTTP.SocketIO.SocketOptions options = new BestHTTP.SocketIO.SocketOptions();
            options.ConnectWith = BestHTTP.SocketIO.Transports.TransportTypes.WebSocket;
            manager = new BestHTTP.SocketIO.SocketManager(new Uri(APIConfiguration.HostURL + APIConfiguration.SocketIORoute), options);
            manager.Socket.On(BestHTTP.SocketIO.SocketIOEventTypes.Connect, OnConnected);
        }
    }

    void OnConnected(BestHTTP.SocketIO.Socket socket, BestHTTP.SocketIO.Packet packet, object[] args) {
        IsConnected = true;
    }
}