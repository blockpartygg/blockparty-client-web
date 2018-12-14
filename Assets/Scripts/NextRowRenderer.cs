using UnityEngine;

public class NextRowRenderer : MonoBehaviour {
	public BoardRaiser BoardRaiser;
	public FloatReference RaiseDuration;

	void Update() {
		Vector3 raiseTranslation = new Vector3(0, BoardRaiser.Elapsed / RaiseDuration.Value);
		transform.position = transform.parent.position + raiseTranslation;
	}
}