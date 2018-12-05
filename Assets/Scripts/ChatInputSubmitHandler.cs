using UnityEngine;
using TMPro;

public class ChatInputSubmitHandler : MonoBehaviour {
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
        ChatManager.Instance.SendChatMessage(PlayerManager.Instance.Name, chatInput.text);
        chatInput.text = "";
    }
}