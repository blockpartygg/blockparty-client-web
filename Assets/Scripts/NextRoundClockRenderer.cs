using UnityEngine;
using TMPro;
using System;

public class NextRoundClockRenderer : MonoBehaviour {
	TMP_Text text;
	public string StringFormat = "Next round starts in {0}:{1:D2}";
	public string GameEndingStringFormat = "Game ends in {0}:{1:D2}";

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Update() {
		TimeSpan timeRemaining = TimeSpan.Zero;

		if(Clock.Instance.Round != ConfigManager.Instance.RoundCount) {
			switch(Clock.Instance.State) {
				case GameManager.GameState.PreRound:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining);
					break;
				case GameManager.GameState.Scoreboard:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining + (ConfigManager.Instance.LeaderboardDuration / 1000) + (ConfigManager.Instance.PreRoundDuration / 1000));
					break;
				case GameManager.GameState.Leaderboard:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining + (ConfigManager.Instance.PreRoundDuration / 1000));
					break;
			}

			text.text = string.Format(StringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
		}
		else {
			switch(Clock.Instance.State) {
				case GameManager.GameState.PreRound:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining);
					break;
				case GameManager.GameState.Scoreboard:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining + (ConfigManager.Instance.PostgameDuration / 1000));
					break;
				case GameManager.GameState.Postgame:
					timeRemaining = TimeSpan.FromSeconds(Clock.Instance.SecondsRemaining);
					break;
			}

			text.text = string.Format(GameEndingStringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
		}
	}
}
