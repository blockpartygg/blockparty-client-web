using UnityEngine;
using TMPro;

public class ModeNameRenderer : MonoBehaviour {
	TMP_Text modeNameText;
    public string TimeAttackName = "Time Attack";
    public string SurvivalName = "Survival";

	void Awake() {
		modeNameText = GetComponent<TMP_Text>();
	}

	void Start() {
        string modeName = "";
        
        switch(Clock.Instance.Mode) {
            case GameManager.GameMode.TimeAttack:
                modeName = TimeAttackName;
                break;
            case GameManager.GameMode.Survival:
                modeName = SurvivalName;
                break;
        }

		modeNameText.text = modeName;
	}
}
