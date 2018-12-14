using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleSceneInputValidator : MonoBehaviour {
    public TMP_InputField PlayerNameInputField;
    public Button PlayButton;

    void Start() {
        Validate();
    }

    public void Validate() {
        PlayButton.interactable = !string.IsNullOrEmpty(PlayerNameInputField.text) &&
            PlayerNameInputField.text.Length < 20;
    }
}