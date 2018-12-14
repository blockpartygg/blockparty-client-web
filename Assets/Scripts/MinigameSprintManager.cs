using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class MinigameSprintManager : MonoBehaviour {
	public Game Game;
	public BoardController BoardController;
	public BlockManager BlockManager;
	public TMP_Text EliminatedText;
	public ScoreSubmitter ScoreSubmitter;
	public AnnouncementPlayer AnnouncementPlayer;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		PlayAnnouncement();
	}

	public void HandleTimeExpired() {
		if(Game.State != GameState.Scoreboard) {
			PlayAnnouncement();
		}
		
		if(Game.State == GameState.PostMinigame) {
			EndGame();
			if(ScoreSubmitter != null) {
				ScoreSubmitter.SubmitScoreAsync();
			}
		}
	}

	void PlayAnnouncement() {
		AnnouncementType announcementType = AnnouncementType.None;
		
		switch(Game.State) {
			case GameState.PreMinigame:
				announcementType = AnnouncementType.PregameStart;
				break;
			case GameState.InMinigame:
				announcementType = AnnouncementType.InGameStart;
				break;
			case GameState.PostMinigame:
				announcementType = AnnouncementType.PostgameStart;
				break;
			default:
				return;
		}

		if(announcementType != AnnouncementType.None) {
			AnnouncementPlayer.Play(announcementType);
		}
	}

	public void EliminatePlayer() {
		BlockManager.KillBlocks();
		EliminatedText.enabled = true;
		EliminatedText.transform.DOMoveY(1f, 1f);
		EliminatedText.DOFade(1f, 1f);
	}

	public void EndGame() {
		if(BoardController != null) {
			BoardController.enabled = false;
		}
	}
}