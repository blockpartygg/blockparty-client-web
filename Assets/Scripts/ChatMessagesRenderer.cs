using UnityEngine;
using System;
using TMPro;

public class ChatMessagesRenderer : MonoBehaviour {
    public ChatManager ChatManager;
    public GameObject ChatMessagePrefab;
    public string MessageStringFormat = "<b><color=#17A589>{0}</color></b> {1}";
    GameObject content;

    void Start() {
        content = transform.Find("Viewport").Find("Content").gameObject;

        PopulateMessages();

        ChatManager.MessageAdded += HandleMessageAdded;
    }

    void PopulateMessages() {
        foreach(ChatMessage message in ChatManager.Messages) {
            AddMessage(message.PlayerName, message.Message);
        }
    }

    void HandleMessageAdded(object sender, EventArgs args) {
        ChatMessage newMessage = ChatManager.Messages[ChatManager.Messages.Count - 1];
        AddMessage(newMessage.PlayerName, newMessage.Message);
    }

    void AddMessage(string playerName, string message) {
        GameObject chatMessageObject = Instantiate(ChatMessagePrefab, Vector3.zero, Quaternion.identity);
        chatMessageObject.transform.Find("Player Image").Find("Player Name Initials").GetComponent<TMP_Text>().text = playerName.Substring(0, 1);
        chatMessageObject.transform.Find("Message").GetComponent<TMP_Text>().text = string.Format(MessageStringFormat, playerName, message);
        content = transform.Find("Viewport").Find("Content").gameObject;
        chatMessageObject.transform.SetParent(content.transform, false);
    }

    void OnDestroy() {
        ChatManager.MessageAdded -= HandleMessageAdded;
    }
}