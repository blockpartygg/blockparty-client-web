using UnityEngine;
using System;

public class Clock : Singleton<Clock> {
    public GameManager.GameState State;
    DateTime endTime;
    public float SecondsRemaining;
    public int Round;
    public GameManager.GameMode Mode;
    
    public event EventHandler TimeExpired;

    public void SyncToServerClock() {
        GameManager.Instance.GameUpdated += HandleGameUpdated;
        GameManager.Instance.FetchGameAsync();
    }

    void HandleGameUpdated(object sender, EventArgs args) {
        State = GameManager.Instance.State;
        endTime = GameManager.Instance.EndTime;
        Round = GameManager.Instance.Round;
        Mode = GameManager.Instance.Mode;
        
        GameManager.Instance.GameUpdated -= HandleGameUpdated;
    }

    void Update() {
        // Update time remaining in current state
        TimeSpan timeRemaining = endTime - DateTime.Now;
        SecondsRemaining = (float)timeRemaining.TotalSeconds;

        // When it's the state end time, set the next state based on the current one
        if(SecondsRemaining <= 0) {
            switch(State) {
                case GameManager.GameState.Pregame:
                    SetupPreRound();
                    break;
                case GameManager.GameState.PreRound:
                    State = GameManager.GameState.PreMinigame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PreMinigameDuration);
                    break;
                case GameManager.GameState.PreMinigame:
                    State = GameManager.GameState.InMinigame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.InMinigameDuration);
                    break;
                case GameManager.GameState.InMinigame:
                    State = GameManager.GameState.PostMinigame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PostMinigameDuration);
                    break;
                case GameManager.GameState.PostMinigame:
                    State = GameManager.GameState.Scoreboard;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.ScoreboardDuration);
                    break;
                case GameManager.GameState.Scoreboard:
                    if(Round < ConfigManager.Instance.RoundCount) {
                        State = GameManager.GameState.Leaderboard;
                        endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.LeaderboardDuration);
                    } else {
                        State = GameManager.GameState.Postgame;
                        endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PostgameDuration);
                    }
                    break;
                case GameManager.GameState.Leaderboard:
                    SetupPreRound();
                    break;
                case GameManager.GameState.Postgame:
                    State = GameManager.GameState.Pregame;
                    endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PregameDuration);
                    Round = 0;
                    Mode = GameManager.GameMode.None;
                    break;
            }

            if(TimeExpired != null) {
                TimeExpired(this, null);
            }
        }
    }

    void SetupPreRound() {
        State = GameManager.GameState.PreRound;
        endTime = DateTime.Now + TimeSpan.FromMilliseconds(ConfigManager.Instance.PreRoundDuration);
        Round++;
        Mode = Mode == GameManager.GameMode.None || Mode == GameManager.GameMode.Survival ? GameManager.GameMode.TimeAttack : GameManager.GameMode.Survival;
    }
}