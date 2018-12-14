using UnityEngine;
using UnityEngine.Analytics;
using System;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public Game Game;
    public bool IsSyncingToClockState;
    public string TitleSceneName = "Title";
    public string PregameSceneName = "Pregame";
    public string PreRoundSceneName = "PreRound";
    public string MinigameSceneName = "Minigame";
    public string ScoreboardSceneName = "Scoreboard";
    public string LeaderboardSceneName = "Leaderboard";
    public string PostgameSceneName = "Postgame";
    public string PersistentLeaderboardsSceneName = "PersistentLeaderboards";
    bool isLoadingScene;

    public void HandleTimeExpired() {
        if(IsSyncingToClockState) {
            SyncToClockState();
        }
    }

    void SyncToClockState() {
        string sceneToLoad = "";
        switch(Game.State) {
            case GameState.Pregame:
                sceneToLoad = PregameSceneName;
                break;
            case GameState.PreRound:
                sceneToLoad = PreRoundSceneName;
                break;
            case GameState.PreMinigame:
            case GameState.InMinigame:
            case GameState.PostMinigame:
                sceneToLoad = MinigameSceneName;
                break;
            case GameState.Scoreboard:
                sceneToLoad = ScoreboardSceneName;
                break;
            case GameState.Leaderboard:
                sceneToLoad = LeaderboardSceneName;
                break;
            case GameState.Postgame:
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
            AnalyticsEvent.ScreenVisit(name);
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

    public void SetSyncToClockState(bool isSyncingToClockState) {
        IsSyncingToClockState = isSyncingToClockState;

        if(isSyncingToClockState) {
            SyncToClockState();
        }
        else {
            LoadSceneAsync(TitleSceneName);
        }
    }   
}