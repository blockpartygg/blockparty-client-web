using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownRenderer : MonoBehaviour {
    public Game Game;
    public float SecondsRemainingToShow = 120f;
    public TMP_Text CountdownLabel;
    public Image CountdownBackground;
    public TMP_Text CountdownText;

    void Update() {
        if(Game.SecondsRemaining <= SecondsRemainingToShow) {
            CountdownLabel.enabled = true;
            CountdownBackground.enabled = true;
            CountdownText.enabled = true;
        }
        else {
            CountdownLabel.enabled = false;
            CountdownBackground.enabled = false;
            CountdownText.enabled = false;
        }
    }
}