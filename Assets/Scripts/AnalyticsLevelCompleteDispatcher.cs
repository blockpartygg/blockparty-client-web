using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsLevelCompleteDispatcher : MonoBehaviour {
    public void DispatchLevelComplete() {
        AnalyticsEvent.LevelComplete("Block Attack");
    }
}