using UnityEngine;

public class BlockMatcher : MonoBehaviour {
    public Block Block;
    public BlockData BlockData;
    public BlockClearer Clearer;
    public BlockEmptier Emptier;
    public float Elapsed;

    public void Match(int matchedBlockCount, int delayCounter) {
        Block.State = BlockState.Matched;
        Elapsed = 0f;
        Clearer.DelayDuration = (matchedBlockCount - delayCounter) * BlockData.ClearDelayInterval;
        Emptier.DelayDuration = delayCounter * BlockData.EmptyDelayInterval;
    }

    void Update() {
        if(Block.State == BlockState.Matched) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= BlockData.MatchDuration) {
                Clearer.Clear();
            }
        }
    }
}