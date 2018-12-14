using UnityEngine;
using UnityEngine.UI;

public class PersistentLeaderboardToggleHandler : MonoBehaviour {
    Toggle toggle;
    public GameObject View;

    void Awake() {
        toggle = GetComponent<Toggle>();
    }

    public void HandleValueChanged() {
        View.SetActive(toggle.isOn);
    }
}