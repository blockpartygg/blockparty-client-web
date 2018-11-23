using UnityEngine;
using System;

public class Clock : Singleton<Clock> {
    public GameManager.GameState State;
    public float SecondsRemaining;
    public event EventHandler TimeExpired;
    DateTime endTime;

    public void SyncToServerClock() {
        GameManager.Instance.GameUpdated += HandleGameUpdated;
        GameManager.Instance.FetchGameAsync();
    }

    void HandleGameUpdated(object sender, EventArgs args) {
        State = GameManager.Instance.State;
        endTime = GameManager.Instance.EndTime;
        GameManager.Instance.GameUpdated -= HandleGameUpdated;
    }

    void Update() {
        // Update time remaining in current state
        TimeSpan timeRemaining = endTime - DateTime.Now;
        SecondsRemaining = (float)timeRemaining.TotalSeconds;

        // When it's the state end time, set the next state
        if(SecondsRemaining <= 0) {
            switch(State) {
                case GameManager.GameState.Pregame:
                    State = GameManager.GameState.InGame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.InGameDuration);
                    break;
                case GameManager.GameState.InGame:
                    State = GameManager.GameState.Postgame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PostgameDuration);
                    break;
                case GameManager.GameState.Postgame:
                    State = GameManager.GameState.Scoreboard;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.ScoreboardDuration);
                    break;
                case GameManager.GameState.Scoreboard:
                    State = GameManager.GameState.Pregame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PregameDuration);
                    break;
            }

            if(TimeExpired != null) {
                TimeExpired(this, null);
            }
        }
    }
}