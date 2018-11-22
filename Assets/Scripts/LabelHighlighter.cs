using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelHighlighter : MonoBehaviour {
	TMP_Text text;
	public Color SelectedColor;
	public Color DeselectedColor;

	void Awake() {
		text = GetComponent<TMP_Text>();
	}

	void Start() {
		text.color = DeselectedColor;
	}

	public void HandleSelected() {
		text.color = SelectedColor;
	}

	public void HandleDeselected() {
		text.color = DeselectedColor;
	}
}
