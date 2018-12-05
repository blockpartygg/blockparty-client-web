using UnityEngine;

public enum SlideDirection {
    Left,
    Right,
    None
}

public class BlockSlider : MonoBehaviour {   
    public Block Block;
    public BlockData BlockData;
    public SlideDirection Direction;
    public BlockState TargetState;
    public int TargetType;
    public float Elapsed;
    MatchDetector matchDetector;

    void Awake() {
        matchDetector = GameObject.Find("Minigame").GetComponent<MatchDetector>();
    }

    public void Slide(SlideDirection direction) {
        Block.State = BlockState.Sliding;
        Elapsed = 0f;
        Direction = direction;
    }

    void Update() {
        if(Block.State == BlockState.Sliding) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= BlockData.SlideDuration) {
                Block.State = TargetState;
                Block.Type = TargetType;
                Direction = SlideDirection.None;

                if(Block.State == BlockState.Idle) {
                    matchDetector.RequestMatchDetection(Block);
                }
            }
        }
    }
}