using UnityEngine;
using DG.Tweening;

public class BlockAnimateIn : MonoBehaviour {
    bool isAnimatingIn;
    float delayElapsed;
    float delayDuration;

    void Start() {
        transform.localScale = Vector3.zero;
    }

    public void Play() {
        isAnimatingIn = true;
        delayElapsed = 0;
        delayDuration = Random.Range(0, 1f);
    }

    void FixedUpdate() {
        if(isAnimatingIn) {
            delayElapsed += Time.deltaTime;

            if(delayElapsed >= delayDuration) {
                transform.localScale = Vector3.zero;
                transform.DOScale(1f, 1f);
                isAnimatingIn = false;
            }
        }
    }
}