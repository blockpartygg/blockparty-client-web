using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Leaderboard : ScriptableObject {
    public List<SerializableLeaderboardItem> Scores;

    void Awake() {
        Scores = new List<SerializableLeaderboardItem>();
    }
}