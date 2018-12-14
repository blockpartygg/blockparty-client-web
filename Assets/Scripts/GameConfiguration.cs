using UnityEngine;

[CreateAssetMenu]
public class GameConfiguration : ScriptableObject {
    public int PregameDuration;
    public int PreRoundDuration;
    public int PreMinigameDuration;
    public int InMinigameDuration;
    public int PostMinigameDuration;
    public int ScoreboardDuration;
    public int LeaderboardDuration;
    public int PostgameDuration;
    public int RoundCount;
}