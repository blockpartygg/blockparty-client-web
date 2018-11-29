using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreCountdownRenderer : MonoBehaviour {
    public float SecondsRemainingToHide = 120f;
    public Image PreCountdownImage;
    public TMP_Text PreCountdownLabel;
    public TMP_Text PreCountdownText;

    void Update() {
        if(Clock.Instance.SecondsRemaining <= SecondsRemainingToHide) {
            PreCountdownImage.enabled = false;
            PreCountdownLabel.enabled = false;
            PreCountdownText.enabled = false;
        }
        else {
            PreCountdownImage.enabled = true;
            PreCountdownLabel.enabled = true;
            PreCountdownText.enabled = true;
        }
    }
}