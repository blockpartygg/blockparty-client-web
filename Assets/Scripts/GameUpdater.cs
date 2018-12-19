using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;


public class GameUpdater : MonoBehaviour {
    public Game Game;
    public GameConfiguration GameConfiguration;
    public GameEvent OnTimeExpired;
    public Score Score;

    void Update() {
        // Update time remaining in current state
        TimeSpan timeRemaining = Game.EndTime - DateTime.Now;
        Game.SecondsRemaining = (float)timeRemaining.TotalSeconds;

        // When it's the state end time, set the next state based on the current one
        if(Game.SecondsRemaining <= 0) {
            switch(Game.State) {
                case GameState.Pregame:
                    SetupRoundSetup();
                    break;
                case GameState.RoundSetup:
                    Game.State = GameState.PreMinigame;
                    Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.PreMinigameDuration);
                    Score.Reset();
                    break;
                case GameState.PreMinigame:
                    Game.State = GameState.InMinigame;
                    Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.InMinigameDuration);
                    break;
                case GameState.InMinigame:
                    Game.State = GameState.PostMinigame;
                    Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.PostMinigameDuration);
                    break;
                case GameState.PostMinigame:
                    Game.State = GameState.RoundResults;
                    Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.RoundResultsDuration);
                    break;
                case GameState.RoundResults:
                    if(Game.Round < GameConfiguration.RoundCount) {
                        Game.State = GameState.Scoreboard;
                        Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.ScoreboardDuration);
                    } else {
                        Game.State = GameState.Postgame;
                        Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.PostgameDuration);
                    }
                    break;
                case GameState.Scoreboard:
                    SetupRoundSetup();
                    break;
                case GameState.Postgame:
                    Game.State = GameState.Pregame;
                    Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.PregameDuration);
                    Game.Round = 0;
                    Game.Mode = GameMode.None;
                    break;
            }

            OnTimeExpired.Raise();
        }
    }

    void SetupRoundSetup() {
        Game.State = GameState.RoundSetup;
        Game.EndTime = DateTime.Now + TimeSpan.FromMilliseconds(GameConfiguration.RoundSetupDuration);
        Game.Round++;
        Game.Mode = Game.Mode == GameMode.None || Game.Mode == GameMode.Survival ? GameMode.TimeAttack : GameMode.Survival;
    }
}