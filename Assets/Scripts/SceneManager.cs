using UnityEngine;
using System;
using System.Collections;

public class SceneManager : Singleton<SceneManager> {
    bool isSyncingToClockState;
    bool isLoadingScene;
    public string TitleSceneName = "Title";
    public string PregameSceneName = "Pregame";
    public string PreRoundSceneName = "PreRound";
    public string MinigameSceneName = "Minigame";
    public string ScoreboardSceneName = "Scoreboard";
    public string LeaderboardSceneName = "Leaderboard";
    public string PostgameSceneName = "Postgame";

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
                sceneToLoad = PregameSceneName;
                break;
            case GameManager.GameState.PreRound:
                sceneToLoad = PreRoundSceneName;
                break;
            case GameManager.GameState.PreMinigame:
            case GameManager.GameState.InMinigame:
            case GameManager.GameState.PostMinigame:
                sceneToLoad = MinigameSceneName;
                break;
            case GameManager.GameState.Scoreboard:
                sceneToLoad = ScoreboardSceneName;
                break;
            case GameManager.GameState.Leaderboard:
                sceneToLoad = LeaderboardSceneName;
                break;
            case GameManager.GameState.Postgame:
                sceneToLoad = PostgameSceneName;
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