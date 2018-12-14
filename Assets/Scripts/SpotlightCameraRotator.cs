using UnityEngine;

public class SpotlightCameraRotator : MonoBehaviour {
    float angle;
    public float xSpeed;
    public float ySpeed;

    void Update() {
        angle += Time.deltaTime;
        transform.rotation = Quaternion.Euler(Mathf.Sin(angle * xSpeed) * 30, Mathf.Cos(angle * ySpeed) * 30, 0);
    }
}