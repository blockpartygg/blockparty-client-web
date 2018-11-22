using UnityEngine;
using TMPro;
using System;

public class ScoreboardClockRenderer : MonoBehaviour {
	public Clock Clock;
	TMP_Text text;
	public string StringFormat = "Next game starts in {0}:{1:D2}";

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Update() {
		TimeSpan timeRemaining = Clock.TimeRemaining;

		text.text = string.Format(StringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
	}
}
