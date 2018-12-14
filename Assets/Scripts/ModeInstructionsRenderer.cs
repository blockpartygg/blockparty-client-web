using UnityEngine;
using TMPro;

public class ModeInstructionsRenderer : MonoBehaviour {
	public Game Game;
    TMP_Text modeInstructionsText;
    public string TimeAttackInstructions = "Score as many points as possible before time runs out.";
    public string SurvivalInstructions = "Prevent blocks from reaching the top of the screen for as long as possible.";

	void Awake() {
		modeInstructionsText = GetComponent<TMP_Text>();
	}

	void Start() {
		string modeInstructions = "";

        switch(Game.Mode) {
            case GameMode.TimeAttack:
                modeInstructions = TimeAttackInstructions;
                break;
            case GameMode.Survival:
                modeInstructions = SurvivalInstructions;
                break;
        }

        modeInstructionsText.text = modeInstructions;
	}
}
