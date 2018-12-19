using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Scoreboard : ScriptableObject {
    public List<ScoreboardItem> Items;

    void Awake() {
        Items = new List<ScoreboardItem>();
    }
}