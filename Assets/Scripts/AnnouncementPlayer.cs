using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	Image image;

	void Awake() {
		image = GetComponent<Image>();
	}

	public void Play(AnnouncementType type) {
		if(image == null) {
			return;
		}
		
		switch(type) {
			case AnnouncementType.PregameStart:
				image.sprite = PregameStartImage;
				break;
			case AnnouncementType.InGameStart:
				image.sprite = InGameStartImage;
				break;
			case AnnouncementType.PostgameStart:
				image.sprite = PostgameStartImage;
				break;
			default:
				return;
		}

		image.SetNativeSize();
		transform.localScale = new Vector3(3f, 3f, 3f);
		transform.DOScale(Vector3.one, 0.25f);
		image.color = Color.white;
		image.DOColor(Color.clear, 1.0f).SetDelay(1f);
	}
}
