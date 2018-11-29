using UnityEngine;
using TMPro;
using System;

public class CountdownTimeRemainingRenderer : MonoBehaviour {
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
			TimeSpan countdown = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining);
			text.text = string.Format(StringFormat, countdown.Minutes, countdown.Seconds);
		}
    }
}