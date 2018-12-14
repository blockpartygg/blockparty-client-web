using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Scoreboard : ScriptableObject {
    public List<SerializableScore> Scores;

    void Awake() {
        Scores = new List<SerializableScore>();
    }
}