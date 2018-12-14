using UnityEngine;
using TMPro;

public class ChatInputSubmitHandler : MonoBehaviour {
    public ChatManager ChatManager;
    public PlayerManager PlayerManager;
    TMP_InputField chatInput;

    void Awake() {
        chatInput = GetComponent<TMP_InputField>();
    }   

    public void HandleEndEdit() {
        if(Input.GetKey(KeyCode.Return)) {
            Submit();
        }
    }

    public void Submit() {
        ChatManager.SendChatMessage(PlayerManager.Name, chatInput.text);
        chatInput.text = "";
    }
}