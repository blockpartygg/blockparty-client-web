using UnityEngine;

public class LeaderboardsDisplay : MonoBehaviour {
    public GameObject LoadingScreen;
    public GameObject LeaderboardsScreen;

    public void HandleLeaderboardsUpdated() {
        LoadingScreen.SetActive(false);
        LeaderboardsScreen.SetActive(true);
    }
}