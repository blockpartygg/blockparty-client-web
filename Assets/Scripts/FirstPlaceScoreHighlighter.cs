using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstPlaceScoreHighlighter : MonoBehaviour {
    public PlayerManager PlayerManager;
    public TMP_Text PlayerName;
    public Image PlayerPictureBackground;
    public Image PlayerNameBackground;
    public Color HighlightColor = new Color(0.09f, 0.65f, 0.54f, 1f);

    void Update() {
        if(PlayerName.text == PlayerManager.Name) {
            PlayerPictureBackground.color = HighlightColor;
            PlayerNameBackground.color = HighlightColor;
        }
    }
}