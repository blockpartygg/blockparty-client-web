using UnityEngine;

[CreateAssetMenu]
public class GameConfiguration : ScriptableObject {
    public int PregameDuration;
    public int RoundSetupDuration;
    public int PreMinigameDuration;
    public int InMinigameDuration;
    public int PostMinigameDuration;
    public int RoundResultsDuration;
    public int ScoreboardDuration;
    public int PostgameDuration;
    public int RoundCount;
}