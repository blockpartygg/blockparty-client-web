using UnityEngine;
using TMPro;

public class ModeInstructionsRenderer : MonoBehaviour {
	TMP_Text modeInstructionsText;
    public string TimeAttackInstructions = "Score as many points as possible before time runs out.";
    public string SurvivalInstructions = "Prevent blocks from reaching the top of the screen for as long as possible.";

	void Awake() {
		modeInstructionsText = GetComponent<TMP_Text>();
	}

	void Start() {
		string modeInstructions = "";

        switch(Clock.Instance.Mode) {
            case GameManager.GameMode.TimeAttack:
                modeInstructions = TimeAttackInstructions;
                break;
            case GameManager.GameMode.Survival:
                modeInstructions = SurvivalInstructions;
                break;
        }

        modeInstructionsText.text = modeInstructions;
	}
}
