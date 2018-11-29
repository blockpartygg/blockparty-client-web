using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class MinigameManager : MonoBehaviour {
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
		Clock.Instance.TimeExpired += HandleTimeExpired;
	}

	void HandleTimeExpired(object sender, EventArgs args) {
		if(Clock.Instance.State != GameManager.GameState.Scoreboard) {
			PlayAnnouncement();
		}
		
		if(Clock.Instance.State == GameManager.GameState.PostMinigame) {
			EndGame();
			if(ScoreSubmitter != null) {
				ScoreSubmitter.SubmitScoreAsync();
			}
		}
	}

	void PlayAnnouncement() {
		AnnouncementType announcementType = AnnouncementType.None;
		
		switch(Clock.Instance.State) {
			case GameManager.GameState.PreMinigame:
				announcementType = AnnouncementType.PregameStart;
				break;
			case GameManager.GameState.InMinigame:
				announcementType = AnnouncementType.InGameStart;
				break;
			case GameManager.GameState.PostMinigame:
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