using UnityEngine;
using System;
using System.Collections;

public class SceneManager : Singleton<SceneManager> {
    bool isSyncingToGameState;
    bool isLoadingScene;
    const string TitleSceneName = "Title";
    const string GameSceneName = "Game";
    const string ScoreboardSceneName = "Scoreboard";

    void Start() {
        GameManager.Instance.GameUpdated += HandleGameUpdated;
    }

    void HandleGameUpdated(object sender, EventArgs args) {
        if(isSyncingToGameState) {
            SyncToGameState();
        }
    }

    public void SetSyncToGameState(bool isSyncingToGameState) {
        this.isSyncingToGameState = isSyncingToGameState;

        if(isSyncingToGameState) {
            SyncToGameState();
        }
        else {
            LoadSceneAsync(TitleSceneName);
        }
    }

    void SyncToGameState() {
        string sceneToLoad = "";
        switch(GameManager.Instance.State) {
            case GameManager.GameState.Pregame:
            case GameManager.GameState.InGame:
            case GameManager.GameState.Postgame:
                sceneToLoad = GameSceneName;
                break;
            case GameManager.GameState.Scoreboard:
                sceneToLoad = ScoreboardSceneName;
                break;
        }

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != sceneToLoad) {
            LoadSceneAsync(sceneToLoad);
        }
    }

    public void LoadSceneAsync(string name) {
        if(!isLoadingScene) {
            isLoadingScene = true;
            StartCoroutine(LoadScene(name));
        }
    }

    IEnumerator LoadScene(string name) {
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);

        while(!loadOperation.isDone) {
            yield return null;
        }

        isLoadingScene = false;
    }
}