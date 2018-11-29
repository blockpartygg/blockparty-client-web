using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum AnnouncementType {
	None,
	PregameStart,
	InGameStart,
	PostgameStart
}

public class AnnouncementPlayer : MonoBehaviour {
	public Sprite PregameStartImage;
	public Sprite InGameStartImage;
	public Sprite PostgameStartImage;
	SpriteRenderer spriteRenderer;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Play(AnnouncementType type) {
		if(spriteRenderer == null) {
			return;
		}
		
		switch(type) {
			case AnnouncementType.PregameStart:
				spriteRenderer.sprite = PregameStartImage;
				break;
			case AnnouncementType.InGameStart:
				spriteRenderer.sprite = InGameStartImage;
				break;
			case AnnouncementType.PostgameStart:
				spriteRenderer.sprite = PostgameStartImage;
				break;
			default:
				return;
		}

		transform.localScale = new Vector3(3f, 3f, 3f);
		transform.DOScale(Vector3.one, 0.25f);
		spriteRenderer.color = Color.white;
		spriteRenderer.DOColor(Color.clear, 1.0f).SetDelay(1f);
	}
}
