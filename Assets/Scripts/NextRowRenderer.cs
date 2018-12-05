using UnityEngine;

public class NextRowRenderer : MonoBehaviour {
	public BoardRaiser BoardRaiser;
	public BoardData BoardData;

	void Update() {
		Vector3 raiseTranslation = new Vector3(0, BoardRaiser.Elapsed / BoardData.RaiseDuration);
		transform.position = transform.parent.position + raiseTranslation;
	}
}