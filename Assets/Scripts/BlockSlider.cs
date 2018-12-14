using UnityEngine;

public enum SlideDirection {
    None,
    Left,
    Right,
}

public class BlockSlider : MonoBehaviour {   
    public Block Block;
    public FloatReference SlideDuration;
    public SlideDirection Direction;
    public BlockState TargetState;
    public int TargetType;
    public float Elapsed;
    MatchDetector matchDetector;

    void Awake() {
        GameObject board = GameObject.Find("Board");
        if(board != null) {
            matchDetector = board.GetComponent<MatchDetector>();
        }
        else {
            Debug.Log("Couldn't find Board");
        }
    }

    public void Slide(SlideDirection direction) {
        Block.State = BlockState.Sliding;
        Elapsed = 0f;
        Direction = direction;
    }

    void FixedUpdate() {
        if(Block.State == BlockState.Sliding) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= SlideDuration.Value) {
                Block.State = TargetState;
                Block.Type = TargetType;
                Elapsed = 0;
                Direction = SlideDirection.None;

                if(Block.State == BlockState.Idle) {
                    if(matchDetector != null) {
                        matchDetector.RequestMatchDetection(Block);
                    }
                }
            }
        }
    }
}