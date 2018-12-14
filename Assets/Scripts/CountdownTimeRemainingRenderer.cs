using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class CountdownTimeRemainingRenderer : MonoBehaviour {
    public Game Game;
    public string StringFormat = "{0:D2}:{1:D2}";
    TMP_Text text;
    float updateElapsed;
	const float updateDuration = 1f;

    void Awake() {
        text = GetComponent<TMP_Text>();
    }

    void Update() {
        // Only update once per second
		updateElapsed += Time.deltaTime;
		if(updateElapsed >= updateDuration) {
			updateElapsed = 0f;
			TimeSpan countdown = TimeSpan.FromSeconds(Game.SecondsRemaining);
			text.text = string.Format(StringFormat, countdown.Minutes, countdown.Seconds);
            if(Game.SecondsRemaining <= 10f) {
                text.transform.localScale = new Vector3(2f, 2f, 2f);
                text.transform.DOScale(1f, 0.1f);
            }
		}
    }
}