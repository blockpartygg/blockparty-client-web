using UnityEngine;
using TMPro;
using System;

public class NextRoundClockRenderer : MonoBehaviour {
	public Game Game;
	public GameConfiguration GameConfiguration;
	TMP_Text text;
	public string StringFormat = "Next round starts in {0}:{1:D2}";
	public string GameEndingStringFormat = "Game ends in {0}:{1:D2}";

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Update() {
		TimeSpan timeRemaining = TimeSpan.Zero;

		if(Game.Round != GameConfiguration.RoundCount) {
			switch(Game.State) {
				case GameState.PreRound:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining);
					break;
				case GameState.Scoreboard:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining + (GameConfiguration.LeaderboardDuration / 1000) + (GameConfiguration.PreRoundDuration / 1000));
					break;
				case GameState.Leaderboard:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining + (GameConfiguration.PreRoundDuration / 1000));
					break;
			}

			text.text = string.Format(StringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
		}
		else {
			switch(Game.State) {
				case GameState.PreRound:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining);
					break;
				case GameState.Scoreboard:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining + (GameConfiguration.PostgameDuration / 1000));
					break;
				case GameState.Postgame:
					timeRemaining = TimeSpan.FromSeconds(Game.SecondsRemaining);
					break;
			}

			text.text = string.Format(GameEndingStringFormat, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
		}
	}
}
