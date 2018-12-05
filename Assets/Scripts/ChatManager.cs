using UnityEngine;
using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;

public class ChatMessage {
    public string PlayerName;
    public string Message;
}

public class ChatManager : Singleton<ChatManager> {
    public List<ChatMessage> Messages;
    public event EventHandler MessageAdded;
    public bool IsVisible = false;

    void Awake() {
        Messages = new List<ChatMessage>();
    }

    void Start() {
        SocketManager.Instance.Socket.On("chat", OnChatReceived);
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
        SocketManager.Instance.Socket.Emit("chat", playerName, message);
    }
}