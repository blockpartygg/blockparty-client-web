using UnityEngine;

public class BackButtonNavigator : MonoBehaviour {
    public void Navigate() {
        SceneManager.Instance.LoadSceneAsync(SceneManager.Instance.TitleSceneName);
    }
}