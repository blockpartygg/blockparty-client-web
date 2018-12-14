using UnityEngine;

public class BackButtonNavigator : MonoBehaviour {
    public SceneManager SceneManager;

    public void Navigate() {
        SceneManager.LoadSceneAsync(SceneManager.TitleSceneName);
    }
}