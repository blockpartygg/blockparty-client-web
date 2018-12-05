using UnityEngine;

public class LeaderboardsButtonNavigator : MonoBehaviour {
    public void Navigate() {
        SceneManager.Instance.LoadSceneAsync(SceneManager.Instance.LeaderboardsSceneName);
    }
}