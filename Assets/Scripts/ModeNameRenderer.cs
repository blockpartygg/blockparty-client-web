using UnityEngine;
using TMPro;

public class ModeNameRenderer : MonoBehaviour {
	public Game Game;
    TMP_Text modeNameText;
    public string TimeAttackName = "Time Attack";
    public string SurvivalName = "Survival";

	void Awake() {
		modeNameText = GetComponent<TMP_Text>();
	}

	void Start() {
        string modeName = "";
        
        switch(Game.Mode) {
            case GameMode.TimeAttack:
                modeName = TimeAttackName;
                break;
            case GameMode.Survival:
                modeName = SurvivalName;
                break;
        }

		modeNameText.text = modeName;
	}
}
