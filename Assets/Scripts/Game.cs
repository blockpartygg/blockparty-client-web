using UnityEngine;
using System;

public enum GameState {
    Pregame,
    RoundSetup,
    PreMinigame,
    InMinigame,
    PostMinigame,
    RoundResults,
    Scoreboard,
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