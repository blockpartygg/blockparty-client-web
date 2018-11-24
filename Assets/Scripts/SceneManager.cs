using UnityEngine;
using System;
using System.Collections;

public class SceneManager : Singleton<SceneManager> {
    bool isSyncingToClockState;
    bool isLoadingScene;
    const string TitleSceneName = "Title";
    const string GameSceneName = "Game";
    const string ScoreboardSceneName = "Scoreboard";

    void Start() {
        Clock.Instance.TimeExpired += HandleTimeExpired;
    }

    void HandleTimeExpired(object sender, EventArgs args) {
        if(isSyncingToClockState) {
            SyncToClockState();
        }
    }

    public void SetSyncToClockState(bool isSyncingToClockState) {
        this.isSyncingToClockState = isSyncingToClockState;

        if(isSyncingToClockState) {
            SyncToClockState();
        }
        else {
            LoadSceneAsync(TitleSceneName);
        }
    }

    void SyncToClockState() {
        string sceneToLoad = "";
        switch(Clock.Instance.State) {
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