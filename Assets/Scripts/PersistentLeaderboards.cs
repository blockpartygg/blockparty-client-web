using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class PersistentLeaderboards : ScriptableObject {
    public List<SerializablePersistentLeaderboardItem> TimeAttackLeaderboard;
    public List<SerializablePersistentLeaderboardItem> SurvivalLeaderboard;
    public GameEvent OnUpdated;

    void Awake() {
        TimeAttackLeaderboard = new List<SerializablePersistentLeaderboardItem>();
        SurvivalLeaderboard = new List<SerializablePersistentLeaderboardItem>();
    }
}

