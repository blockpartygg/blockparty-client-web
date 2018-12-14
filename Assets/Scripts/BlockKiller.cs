using UnityEngine;
using DG.Tweening;

public class BlockKiller : MonoBehaviour {
	bool dying;
	float delayElapsed;
	float delayDuration;

	public void Kill() {
		dying = true;
		delayDuration = Random.Range(0, 1f);
	}

	void FixedUpdate() {
		if(dying) {
			delayElapsed += Time.deltaTime;

			if(delayElapsed >= delayDuration) {
				transform.localScale = Vector3.one;
				transform.DOScale(0, 1f);
				dying = false;
			}
		}
	}
}