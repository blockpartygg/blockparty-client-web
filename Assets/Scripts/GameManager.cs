using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SerializableGame {

    public string state;
    public string endTime;
    public int round;
    public string mode;
}

public class GameManager : Singleton<GameManager> {
    public enum GameState {
        Pregame,
        PreRound,
        PreMinigame,
        InMinigame,
        PostMinigame,
        Scoreboard,
        Leaderboard,
        Postgame
    }
    public GameState State;
    public DateTime EndTime;
    public int Round;
    public enum GameMode {
        None,
        TimeAttack,
        Survival
    }
    public GameMode Mode;
    public event EventHandler GameUpdated;
    bool fetchingGame;

    void Start() {
        FetchGameAsync();
    }

    public void FetchGameAsync() {
        if(!fetchingGame) {
            fetchingGame = true;
            StartCoroutine(FetchGame());
        }
    }

    IEnumerator FetchGame() {
        UnityWebRequest request = UnityWebRequest.Get(APIManager.Instance.HostURL + APIManager.Instance.GameRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableGame game = JsonUtility.FromJson<SerializableGame>(text);
            State = (GameState)Enum.Parse(typeof(GameState), game.state, true);
            EndTime = DateTime.Parse(game.endTime);
            Round = game.round;
            Mode = !string.IsNullOrEmpty(game.mode) ? (GameMode)Enum.Parse(typeof(GameMode), game.mode, true) : GameMode.None;

            if(GameUpdated != null) {
                GameUpdated(this, null);
            }
        }

        fetchingGame = false;
    }

    void Update() {
        if(EndTime < DateTime.Now) {
            FetchGameAsync();
        }
    }
}