using UnityEngine;

public class PersistentLeaderboardsDisplay : MonoBehaviour {
    public GameObject LoadingScreen;
    public GameObject PersistentLeaderboardsScreen;

    public void HandleLeaderboardsUpdated() {
        LoadingScreen.SetActive(false);
        PersistentLeaderboardsScreen.SetActive(true);
    }
}