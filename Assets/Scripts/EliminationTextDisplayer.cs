using UnityEngine;
using TMPro;
using DG.Tweening;

public class EliminationTextDisplayer : MonoBehaviour {
    public TMP_Text EliminatedText;

    public void Show() {
		EliminatedText.enabled = true;
		EliminatedText.transform.DOMoveY(1f, 1f);
		EliminatedText.DOFade(1f, 1f);
	}
}

