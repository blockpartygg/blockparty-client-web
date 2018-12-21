using UnityEngine;
using TMPro;

public class TitleInputSubmitter : MonoBehaviour {
    public PlayerManager PlayerManager;
    public SceneManager SceneManager;
    public TMP_InputField NameInputField;

    public void HandleEndEdit() {
        if(Input.GetKey(KeyCode.Return)) {
            Submit();
        }
    }

    public void Submit() {
        PlayerManager.SetName(NameInputField.text);
        SceneManager.SetSyncToClockState(true);
    }
}