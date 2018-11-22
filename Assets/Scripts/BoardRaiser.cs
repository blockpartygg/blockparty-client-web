﻿using UnityEngine;

public class BoardRaiser : MonoBehaviour {
	public MinigameManager MinigameManager;
	public BlockManager BlockManager;
	public Score Score;
	public float Elapsed;
	public float LossElapsed;
	public const float Duration = 10f;
	public const float LossDuration = 3f;
	public MatchDetector MatchDetector;

	bool isForcingRaise;
	const float defaultRaiseRate = 1f;
	const float forcedRaiseRate = 20f;
	bool isLossIncoming;

	public void ForceRaise() {
		isForcingRaise = true;
	}

	void Update() {
		if(GameManager.Instance.Mode == GameManager.GameMode.Survival) {
			float raiseRate;
			if(isForcingRaise) {
				raiseRate = forcedRaiseRate;
			}
			else {
				raiseRate = defaultRaiseRate;	
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

				if(LossElapsed >= LossDuration) {
					MinigameManager.EndGame();
				}
			}
			else {
				Elapsed += raiseRate * Time.deltaTime;

				if(Elapsed >= Duration) {
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
