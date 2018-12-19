using UnityEngine;

public class BoardCursorController : MonoBehaviour {
    public int Column = 2, Row = 5;
    public Game Game;
    public BlockManager BlockManager;
    public BoardRaiser Raiser;
    public AudioSource AudioSource;
	public AudioClip SlideClip;

    void Update() {
        int deltaColumn = 0, deltaRow = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            deltaColumn = -1;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            deltaColumn = 1;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            deltaRow = 1;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            deltaRow = -1;
        }

        // Since cursor is 2 blocks wide, constrain position from 0 to Board.Columns - 2
        if(Column + deltaColumn >= 0 && Column + deltaColumn < BlockManager.Columns - 1) {
            Column += deltaColumn;
        }

        // Since cursor shouldn't access the invisible top row, constrain position from 0 to Board.Rows - 2
        if(Row + deltaRow >= 0 && Row + deltaRow < BlockManager.Rows - 1) {
            Row += deltaRow;
        }

        if(Game.State == GameState.InMinigame) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                if((BlockManager.Blocks[Column, Row].State == BlockState.Idle ||
                    BlockManager.Blocks[Column, Row].State == BlockState.Empty) &&
                    (BlockManager.Blocks[Column + 1, Row].State == BlockState.Idle ||
                    BlockManager.Blocks[Column + 1, Row].State == BlockState.Empty) &&
                    (Row + 1 == BlockManager.Rows || Row + 1 < BlockManager.Rows && 
                    BlockManager.Blocks[Column, Row + 1].State != BlockState.Falling && 
                    BlockManager.Blocks[Column + 1, Row + 1].State != BlockState.Falling &&
                    BlockManager.Blocks[Column, Row + 1].State != BlockState.WaitingToFall &&
                    BlockManager.Blocks[Column + 1, Row + 1].State != BlockState.WaitingToFall)) {
                        SetupSlide(BlockManager.Blocks[Column, Row], SlideDirection.Right);
                        SetupSlide(BlockManager.Blocks[Column + 1, Row], SlideDirection.Left);
                        BlockManager.Blocks[Column, Row].Slider.Slide(SlideDirection.Right);
                        BlockManager.Blocks[Column + 1, Row].Slider.Slide(SlideDirection.Left);
                        AudioSource.clip = SlideClip;
                        AudioSource.pitch = 1f;
                        AudioSource.Play();
                    }
            }

            if(Input.GetKeyDown(KeyCode.Return)) {
                Raiser.ForceRaise();
            }
        }
    }

    void SetupSlide(Block block, SlideDirection direction) {
		Block target = direction == SlideDirection.Left ? BlockManager.Blocks[block.Column - 1, block.Row] : BlockManager.Blocks[block.Column + 1, block.Row];
		block.Slider.TargetState = target.State;
		block.Slider.TargetType = target.Type;
	}
}