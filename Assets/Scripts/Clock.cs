using UnityEngine;
using System;

public class Clock : MonoBehaviour {
    public TimeSpan TimeRemaining;
    public event EventHandler TimeExpired;

    void Update() {
        if(GameManager.Instance.EndTime != DateTime.MinValue) {
            TimeRemaining = GameManager.Instance.EndTime > DateTime.Now ? GameManager.Instance.EndTime - DateTime.Now : TimeSpan.Zero;

            if(TimeRemaining <= TimeSpan.Zero) {
                if(TimeExpired != null) {
                    TimeExpired(this, null);
                }
            }
        }
    }
}