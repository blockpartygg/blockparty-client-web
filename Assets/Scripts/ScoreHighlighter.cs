using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHighlighter : MonoBehaviour {
    public PlayerManager PlayerManager;
    public TMP_Text PlayerName;
    public Image HighlightImage;
    public Color HighlightColor = new Color(0.9647059f, 0.3568628f, 0.7019608f, 0.3607843f);

    void Update() {
        if(PlayerName.text == PlayerManager.Name) {
            HighlightImage.color = HighlightColor;
        }
    }
}