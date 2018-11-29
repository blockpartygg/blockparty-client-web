using UnityEngine;
using TMPro;

public class RoundRenderer : MonoBehaviour {
	TMP_Text roundText;
    public string StringFormat = "Round {0}";

	void Awake() {
		roundText = GetComponent<TMP_Text>();
	}

	void Update() {
		roundText.text = string.Format(StringFormat, Clock.Instance.Round);
	}
}
