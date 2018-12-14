using UnityEngine;

public class BlockMatcher : MonoBehaviour {
    public Block Block;
    public FloatReference ClearDelayInterval;
    public FloatReference EmptyDelayInterval;
    public FloatReference MatchDuration;
    public BlockClearer Clearer;
    public BlockEmptier Emptier;
    public float Elapsed;

    public void Match(int matchedBlockCount, int delayCounter) {
        Block.State = BlockState.Matched;
        Elapsed = 0f;
        Clearer.DelayDuration = (matchedBlockCount - delayCounter) * ClearDelayInterval.Value;
        Emptier.DelayDuration = delayCounter * EmptyDelayInterval.Value;
        Clearer.Pitch = 0.75f + (3 - delayCounter) * 0.25f;
    }

    void FixedUpdate() {
        if(Block.State == BlockState.Matched) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= MatchDuration.Value) {
                Clearer.Clear();
            }
        }
    }
}