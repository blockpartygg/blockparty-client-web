using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClockRenderer : MonoBehaviour {
	public Clock Clock;
	TMP_Text text;
	public string StringFormat = "{0}:{1:D2}<sup>:{2:D3}</sup>";

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Update() {
		TimeSpan timeRemaining = TimeSpan.Zero;

		switch(GameManager.Instance.State) {
			case GameManager.GameState.Pregame:
				timeRemaining = TimeSpan.FromMilliseconds(ConfigManager.Instance.InGameDuration);		
				break;
			case GameManager.GameState.InGame:
				timeRemaining = Clock.TimeRemaining;
				break;
			case GameManager.GameState.Postgame:
				timeRemaining = TimeSpan.Zero;
				break;
		}
		
		text.text = string.Format(StringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
	}
}
