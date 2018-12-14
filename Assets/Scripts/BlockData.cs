using UnityEngine;

[CreateAssetMenu()]
public class BlockData : ScriptableObject {
    public float SlideDuration = 0.1f;
    public float FallDelayDuration = 0.1f;
    public float FallDuration = 0.1f;
    public float MatchDuration = 1f;
    public float ClearDelayInterval = 0.25f;
    public float ClearDuration = 0.25f;
    public float EmptyDelayInterval = 0.25f;
    public float EndAnimationGravity = -0.005f;
}