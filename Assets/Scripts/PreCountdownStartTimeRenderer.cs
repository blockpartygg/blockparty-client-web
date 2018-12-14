using UnityEngine;
using TMPro;
using System;

public class PreCountdownStartTimeRenderer : MonoBehaviour {
    public Game Game;
    public string StringFormat = "Next game starts at {0}";

    void Start() {
        DateTime startTime = DateTime.Now + TimeSpan.FromSeconds(Game.SecondsRemaining);
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = string.Format(StringFormat, startTime.ToShortTimeString());
    }
}