using UnityEngine;
using System;

public class SocketManager : Singleton<SocketManager> {
    BestHTTP.SocketIO.SocketManager manager;

    public BestHTTP.SocketIO.Socket Socket {
        get {
            return manager.Socket;
        }
    }

    public bool IsConnected;
    public event EventHandler Connected;

    public void Connect() {
        if(!IsConnected) {
            BestHTTP.SocketIO.SocketOptions options = new BestHTTP.SocketIO.SocketOptions();
            options.ConnectWith = BestHTTP.SocketIO.Transports.TransportTypes.WebSocket;
            manager = new BestHTTP.SocketIO.SocketManager(new Uri(APIManager.Instance.HostURL + APIManager.Instance.SocketIORoute), options);
            manager.Socket.On(BestHTTP.SocketIO.SocketIOEventTypes.Connect, OnConnected);
        }
    }

    void OnConnected(BestHTTP.SocketIO.Socket socket, BestHTTP.SocketIO.Packet packet, object[] args) {
        IsConnected = true;
        if(Connected != null) {
            Connected(this, null);
        }
    }
}