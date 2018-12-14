using UnityEngine;
using System;

public enum GameState {
    Pregame,
    PreRound,
    PreMinigame,
    InMinigame,
    PostMinigame,
    Scoreboard,
    Leaderboard,
    Postgame
}

public enum GameMode {
    None,
    TimeAttack,
    Survival,
    Sprint
}

[CreateAssetMenu]
public class Game : ScriptableObject {
    public GameState State;
    public DateTime EndTime;
    public float SecondsRemaining;
    public int Round;
    public GameMode Mode;
}