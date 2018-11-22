using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SerializableGame {
    [Serializable]
    public class SerializableGameScore {
        public string id;
        public int score;
    }

    public string state;
    public string endTime;
    public string mode;
    public List<SerializableGameScore> scoreboard;
}

public class GameManager : Singleton<GameManager> {
    public enum GameState {
        Pregame,
        InGame,
        Postgame,
        Scoreboard
    }
    public GameState State;
    public DateTime EndTime;
    public enum GameMode {
        TimeAttack,
        Survival
    }
    public GameMode Mode;
    public List<SerializableGame.SerializableGameScore> Scoreboard;
    public event EventHandler GameUpdated;

    void Awake() {
        Scoreboard = new List<SerializableGame.SerializableGameScore>();
    }

    void Start() {
        FetchGameAsync();
    }

    public void FetchGameAsync() {
        StartCoroutine(FetchGame());
    }

    IEnumerator FetchGame() {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:1337/game");
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableGame game = JsonUtility.FromJson<SerializableGame>(text);
            State = (GameState)Enum.Parse(typeof(GameState), game.state, true);
            EndTime = DateTime.Parse(game.endTime);
            Mode = (GameMode)Enum.Parse(typeof(GameMode), game.mode, true);
            for(int scoreboardIndex = 0; scoreboardIndex < game.scoreboard.Count; scoreboardIndex++) {
                Debug.Log("game.scoreboard[" + scoreboardIndex + "].id = " + game.scoreboard[scoreboardIndex].id);
                Debug.Log("game.scoreboard[" + scoreboardIndex + "].score = " + game.scoreboard[scoreboardIndex].score);
            }

            if(GameUpdated != null) {
                GameUpdated(this, null);
            }
        }
    }

    void Update() {
        if(EndTime < DateTime.Now) {
            FetchGameAsync();
        }
    }
}