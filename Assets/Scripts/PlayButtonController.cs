using UnityEngine;
using TMPro;

public class PlayButtonController : MonoBehaviour {
    public PlayerManager PlayerManager;
    public SceneManager SceneManager;
    public TMP_InputField NameInputField;

    public void Play() {
        PlayerManager.SetName(NameInputField.text);
        SceneManager.SetSyncToClockState(true);
    }
}