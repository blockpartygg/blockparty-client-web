using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Leaderboards : ScriptableObject {
    public List<LeaderboardItem> TimeAttackLeaderboardItems;
    public List<LeaderboardItem> SurvivalLeaderboardItems;

    void Awake() {
        TimeAttackLeaderboardItems = new List<LeaderboardItem>();
        SurvivalLeaderboardItems = new List<LeaderboardItem>();
    }
}

