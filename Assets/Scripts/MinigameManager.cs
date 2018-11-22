﻿using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class MinigameManager : MonoBehaviour {
	public BoardController BoardController;
	public BlockManager BlockManager;
	public TMP_Text EliminatedText;
	public ScoreSubmitter ScoreSubmitter;
	public Clock Clock;
	public AnnouncementPlayer AnnouncementPlayer;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		PlayAnnouncement();
		Clock.TimeExpired += HandleTimeExpired;
	}

	void HandleTimeExpired(object sender, EventArgs args) {
		if(GameManager.Instance.State != GameManager.GameState.Scoreboard) {
			PlayAnnouncement();
		}
		
		if(GameManager.Instance.State == GameManager.GameState.Postgame) {
			EndGame();
			ScoreSubmitter.SubmitScoreAsync();
		}
	}

	void PlayAnnouncement() {
		AnnouncementType announcementType = AnnouncementType.None;
		
		switch(GameManager.Instance.State) {
			case GameManager.GameState.Pregame:
				announcementType = AnnouncementType.PregameStart;
				break;
			case GameManager.GameState.InGame:
				announcementType = AnnouncementType.InGameStart;
				break;
			case GameManager.GameState.Postgame:
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
		BoardController.enabled = false;
	}

	void Update() {
		if(Input.GetKeyDown("space")) {
			ScoreSubmitter.SubmitScoreAsync();
		}
	}
}