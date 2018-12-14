using UnityEngine;

[CreateAssetMenu()]
public class BoardData : ScriptableObject {
    public float RaiseDuration = 10f;
    public float RaiseLossCountdownDuration = 3f;
    public float MinimumRaiseRate = 1f;
    public float MaximumRaiseRate = 10f;
    public float ManualRaiseRate = 20f;
}