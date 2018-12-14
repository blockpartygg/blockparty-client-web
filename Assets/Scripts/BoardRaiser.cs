using UnityEngine;

public class BoardRaiser : MonoBehaviour {
	public Game Game;
	public GameConfiguration GameConfiguration;
	public BlockManager BlockManager;
	public FloatReference RaiseDuration;
	public FloatReference RaiseLossCountdownDuration;
	public FloatReference MinimumRaiseRate;
	public FloatReference MaximumRaiseRate;
	public FloatReference ManualRaiseRate;
	public Score Score;
	public float Elapsed;
	public float LossElapsed;
	public MatchDetector MatchDetector;
	public BoardCursorController Controller;
	public GameEvent OnPlayerEliminated;

	bool isForcingRaise;
	[SerializeField] float raiseRate = 1f;
	bool isLossIncoming;

	public void ForceRaise() {
		isForcingRaise = true;
	}

	void FixedUpdate() {
		if(Game.State == GameState.InMinigame && Game.Mode == GameMode.Survival) {
			if(isForcingRaise) {
				raiseRate = ManualRaiseRate.Value;
			}
			else {
				// Scale raise rate based on remaining time
				raiseRate = Mathf.Lerp(MinimumRaiseRate.Value, MaximumRaiseRate.Value, ((GameConfiguration.InMinigameDuration / 1000) - Game.SecondsRemaining) / (GameConfiguration.InMinigameDuration / 1000));	
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

				if(LossElapsed >= RaiseLossCountdownDuration.Value) {
					OnPlayerEliminated.Raise();
				}
			}
			else {
				Elapsed += raiseRate * Time.deltaTime;

				if(Elapsed >= RaiseDuration.Value) {
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

					if(Controller.Row < BlockManager.Rows - 2) {
						Controller.Row++;
					}
				}

				LossElapsed = 0;
			}

			isLossIncoming = false;	
		}
	}
}
