using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatInputValidator : MonoBehaviour {
    TMP_InputField chatInput;
    public Button SubmitButton;

    void Awake() {
        chatInput = GetComponent<TMP_InputField>();
    }

    void Start() {
        Validate();
    }

    public void Validate() {
        SubmitButton.interactable = !string.IsNullOrEmpty(chatInput.text);
    }
}