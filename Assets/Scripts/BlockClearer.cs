using UnityEngine;

public class BlockClearer : MonoBehaviour {
    public Block Block;
    public FloatReference ClearDuration;
    public BlockEmptier Emptier;
    public Score Score;
    public AudioSource AudioSource;
    float delayElapsed;
    public float DelayDuration;
    public float Elapsed;
    public float Pitch;
    bool isEndingGame;

    public void Clear(bool isEndingGame = false) {
        Block.State = BlockState.WaitingToClear;
        delayElapsed = 0f;
        this.isEndingGame = isEndingGame;
    }

    void FixedUpdate() {
        if(Block.State == BlockState.WaitingToClear) {
            delayElapsed += Time.deltaTime;
            
            if(delayElapsed >= DelayDuration && !isEndingGame) {
                Block.State = BlockState.Clearing;
                Elapsed = 0f;
                Score.ScoreMatch();
                AudioSource.pitch = Pitch;
                AudioSource.Play();
            }
        }

        if(Block.State == BlockState.Clearing) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= ClearDuration.Value) {
                Emptier.Empty();
            }
        }
    }
}