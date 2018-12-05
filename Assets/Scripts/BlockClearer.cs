using UnityEngine;

public class BlockClearer : MonoBehaviour {
    public Block Block;
    public BlockData BlockData;
    public BlockEmptier Emptier;
    public Score Score;
    public AudioSource AudioSource;
    float delayElapsed;
    public float DelayDuration;
    public float Elapsed;
    bool isEndingGame;

    void Awake() {
        Score = GameObject.Find("Minigame").GetComponent<Score>();
    }

    public void Clear(bool isEndingGame = false) {
        Block.State = BlockState.WaitingToClear;
        delayElapsed = 0f;
        this.isEndingGame = isEndingGame;
    }

    void Update() {
        if(Block.State == BlockState.WaitingToClear) {
            delayElapsed += Time.deltaTime;
            
            if(delayElapsed >= DelayDuration && !isEndingGame) {
                Block.State = BlockState.Clearing;
                Elapsed = 0f;
                Score.ScoreMatch();
                AudioSource.Play();
            }
        }

        if(Block.State == BlockState.Clearing) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= BlockData.ClearDuration) {
                Emptier.Empty();
            }
        }
    }
}