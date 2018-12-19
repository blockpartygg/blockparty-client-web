using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsGameOverDispatcher : MonoBehaviour {
    public void DispatchGameOver() {
        AnalyticsEvent.GameOver();
    }
}