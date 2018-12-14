using UnityEngine;
using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;

public class ChatMessage {
    public string PlayerName;
    public string Message;
}

[CreateAssetMenu]
public class ChatManager : ScriptableObject {
    public SocketManager SocketManager;
    public List<ChatMessage> Messages;
    public event EventHandler MessageAdded;
    public bool IsVisible = false;

    void OnEnable() {
        Messages = new List<ChatMessage>();
        if(SocketManager.IsConnected && SocketManager.Socket != null) {
            SocketManager.Socket.On("chat", OnChatReceived);
        }
    }

    void OnChatReceived(Socket socket, Packet packet, params object[] args) {
        Dictionary<string, object> message = (Dictionary<string, object>)args[0];
        
        ChatMessage newMessage = new ChatMessage();
        newMessage.PlayerName = (string)message["playerName"];
        newMessage.Message = (string)message["message"];
        Messages.Add(newMessage);
        
        if(MessageAdded != null) {
            MessageAdded(this, null);
        }
    }

    public void SendChatMessage(string playerName, string message) {
        if(SocketManager.Socket != null) {
            SocketManager.Socket.Emit("chat", playerName, message);
        }
        else {
            Debug.Log("SocketManager.Socket is null");
        }
    }
}