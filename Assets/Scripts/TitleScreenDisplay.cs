using UnityEngine;

public class TitleScreenDisplay : MonoBehaviour {
    public GameFetcher GameFetcher;
    public GameObject LoadingScreen;
    public GameObject TitleScreen;

    void Start() {
        LoadingScreen.SetActive(true);
        TitleScreen.SetActive(false);
    }

    public void HandleGameUpdated() {
        LoadingScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }
}