using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsGameStartDispatcher : MonoBehaviour {
    public void DispatchGameStart() {
        AnalyticsEvent.GameStart();
    }
}