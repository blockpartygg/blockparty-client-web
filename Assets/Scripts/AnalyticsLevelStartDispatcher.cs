using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsLevelStartDispatcher : MonoBehaviour {
    public void DispatchLevelStart() {
        AnalyticsEvent.LevelStart("Block Attack");
    }
}