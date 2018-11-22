using UnityEngine;
using TMPro;

public class PlayButtonController : MonoBehaviour {
    public TMP_InputField NameInputField;

    public void Play() {
        PlayerManager.Instance.SetName(NameInputField.text);
        SceneManager.Instance.SetSyncToGameState(true);
    }
}