using UnityEngine;

public class BlockKiller : MonoBehaviour {
	public BlockData BlockData;
	bool dying;
	float delayElapsed;
	float delayDuration;
	float velocity = 0;

	public void Kill() {
		dying = true;
		delayDuration = Random.Range(0, 1f);
	}

	void FixedUpdate() {
		if(dying) {
			delayElapsed += Time.deltaTime;

			if(delayElapsed >= delayDuration) {
				velocity += BlockData.EndAnimationGravity;
				transform.Translate(new Vector3(0, velocity));
			}
		}
	}
}