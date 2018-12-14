using UnityEngine;

public class BoardCursorRenderer : MonoBehaviour {
    public BoardCursorController CursorController;
    public BoardRaiser Raiser;
    public FloatReference RaiseDuration;

    void FixedUpdate() {
        transform.position = transform.parent.position + new Vector3(CursorController.Column, CursorController.Row) + new Vector3(0, Raiser.Elapsed / RaiseDuration.Value);
    }
}