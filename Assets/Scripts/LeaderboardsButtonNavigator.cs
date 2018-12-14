using UnityEngine;

public class LeaderboardsButtonNavigator : MonoBehaviour {
    public SceneManager SceneManager;

    public void Navigate() {
        SceneManager.LoadSceneAsync(SceneManager.PersistentLeaderboardsSceneName);
    }
}