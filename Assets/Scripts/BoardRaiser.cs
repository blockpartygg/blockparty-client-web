using UnityEngine;

public class BoardRaiser : MonoBehaviour {
	public BoardData BoardData;
	public MinigameManager MinigameManager;
	public BlockManager BlockManager;
	public Score Score;
	public float Elapsed;
	public float LossElapsed;
	public MatchDetector MatchDetector;

	bool isForcingRaise;
	[SerializeField] float raiseRate = 1f;
	bool isLossIncoming;

	public void ForceRaise() {
		isForcingRaise = true;
	}

	void Update() {
		if(Clock.Instance.State == GameManager.GameState.InMinigame && Clock.Instance.Mode == GameManager.GameMode.Survival) {
			if(isForcingRaise) {
				raiseRate = BoardData.ManualRaiseRate;
			}
			else {
				// Scale raise rate based on remaining time
				raiseRate = Mathf.Lerp(BoardData.MinimumRaiseRate, BoardData.MaximumRaiseRate, ((ConfigManager.Instance.InMinigameDuration / 1000) - Clock.Instance.SecondsRemaining) / (ConfigManager.Instance.InMinigameDuration / 1000));	
			}
			
			for(int column = 0; column < BlockManager.Columns; column++) {
				for(int row = 0; row < BlockManager.Rows; row++) {
					if(BlockManager.Blocks[column, row].State != BlockState.Empty &&
						BlockManager.Blocks[column, row].State != BlockState.Idle &&
						BlockManager.Blocks[column, row].State != BlockState.Sliding) {
						raiseRate = 0;
					}

					if(row == BlockManager.Rows - 2) {
						if(BlockManager.Blocks[column, row].State != BlockState.Empty) {
							isLossIncoming = true;
						}
					}
				}
			}

			if(isLossIncoming) {
				LossElapsed += raiseRate * Time.deltaTime;

				if(LossElapsed >= BoardData.RaiseLossCountdownDuration) {
					MinigameManager.EliminatePlayer();
					MinigameManager.EndGame();
				}
			}
			else {
				Elapsed += raiseRate * Time.deltaTime;

				if(Elapsed >= BoardData.RaiseDuration) {
					Elapsed = 0f;

					for(int column = 0; column < BlockManager.Columns; column++) {
						for(int row = BlockManager.Rows - 2; row >= 1; row--) {
							BlockManager.Blocks[column, row].Garbage.Width = BlockManager.Blocks[column, row - 1].Garbage.Width;
							BlockManager.Blocks[column, row].Garbage.Height = BlockManager.Blocks[column, row - 1].Garbage.Height;
							BlockManager.Blocks[column, row].Garbage.IsNeighbor = BlockManager.Blocks[column, row - 1].Garbage.IsNeighbor;
							BlockManager.Blocks[column, row].State = BlockManager.Blocks[column, row - 1].State;
							BlockManager.Blocks[column, row].Type = BlockManager.Blocks[column, row - 1].Type;
						}

						BlockManager.Blocks[column, 0].State = BlockManager.NewRowBlocks[column].State;
						BlockManager.Blocks[column, 0].Type = BlockManager.NewRowBlocks[column].Type;

						MatchDetector.RequestMatchDetection(BlockManager.Blocks[column, 0]);
					}

					BlockManager.CreateNewRowBlocks();

					if(isForcingRaise) {
						Score.ScoreRaise();

						isForcingRaise = false;
					}
				}

				LossElapsed = 0;
			}

			isLossIncoming = false;	
		}
	}
}
