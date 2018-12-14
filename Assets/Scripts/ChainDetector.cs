using UnityEngine;
using System.Collections.Generic;

public class ChainDetector : MonoBehaviour {
	public BlockManager BlockManager;
	public Score Score;
	public int ChainLength;
	public List<Block> chainContributingBlocks;

	void Awake() {
		chainContributingBlocks = new List<Block>();
	}

	public void AddChainContributingBlock(Block block) {
		chainContributingBlocks.Add(block);
	}

	public void IncrementChain() {
		ChainLength++;
		Score.ScoreChain(ChainLength);
	}

	void FixedUpdate() {
		bool stopChain = true;

		for(int column = 0; column < BlockManager.Columns; column++) {
			for(int row = 0; row < BlockManager.Rows; row++) {
				if(BlockManager.Blocks[column, row].Chainer.JustEmptied) {
					for(int chainEligibleRow = row + 1; chainEligibleRow < BlockManager.Rows; chainEligibleRow++) {
						if(BlockManager.Blocks[column, chainEligibleRow].State == BlockState.Idle) {
							BlockManager.Blocks[column, chainEligibleRow].Chainer.ChainEligible = true;
						}
					}
				}

				BlockManager.Blocks[column, row].Chainer.JustEmptied = false;
			}
		}

		for(int column = 0; column < BlockManager.Columns; column++) {
			for(int row = 0; row < BlockManager.Rows; row++) {
				if(BlockManager.Blocks[column, row].Chainer.ChainEligible) {
					stopChain = false;
				}
			}
		}

		for(int index = chainContributingBlocks.Count - 1; index >= 0; index--) {
			if(chainContributingBlocks[index].State == BlockState.Matched || chainContributingBlocks[index].State == BlockState.WaitingToClear || chainContributingBlocks[index].State == BlockState.Clearing || chainContributingBlocks[index].State == BlockState.WaitingToEmpty) {
				stopChain = false;
			}
			else {
				chainContributingBlocks.Remove(chainContributingBlocks[index]);
			}
		}

		if(ChainLength > 1 && stopChain) {
			if(ChainLength > 1) {
				// Todo: play fanfare
			}

			ChainLength = 1;
		}
	}
}