using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockRenderer : MonoBehaviour {
	public TMP_Text ClockText;

	void Update() {
		TimeSpan timeRemaining = GameManager.Instance.EndTime > DateTime.Now ? GameManager.Instance.EndTime - DateTime.Now : TimeSpan.Zero;
		ClockText.text = string.Format("{0}:{1:D2}<sup>:{2:D3}</sup>", timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
	}
}
