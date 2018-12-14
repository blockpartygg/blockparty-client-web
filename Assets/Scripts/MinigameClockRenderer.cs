using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameClockRenderer : MonoBehaviour {
	public Game Game;
	public GameConfiguration GameConfiguration;
	TMP_Text text;
	public string StringFormat = "{0}:{1:D2}<sup>:{2:D3}</sup>";

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Update() {
		TimeSpan timeRemaining = TimeSpan.Zero;

		switch(Game.State) {
			case GameState.PreMinigame:
				timeRemaining = TimeSpan.FromMilliseconds(GameConfiguration.InMinigameDuration);		
				break;
			case GameState.InMinigame:
				timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining);
				break;
			case GameState.PostMinigame:
				timeRemaining = TimeSpan.Zero;
				break;
		}
		
		text.text = string.Format(StringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
	}
}
