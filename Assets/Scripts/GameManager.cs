using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public enum GameState {
    Playing,
    Scoreboard
}

public enum GameMode {
    TimeAttack,
    Survival
}

[Serializable]
public class Game {
    [Serializable]
    public class State {
        public string id;
        public int duration;
    }

    [Serializable]
    public class Mode {
        public string id;
        public string name;
    }

    [Serializable]
    public class GameScore {
        public string id;
        public int score;
    }

    public State state;
    public string endTime;
    public Mode mode;
    public List<GameScore> scoreboard;
}

public class GameManager : Singleton<GameManager> {
    public GameState State;
    public int StateDuration;
    public DateTime EndTime;
    public GameMode Mode;
    public string ModeName;
    public Dictionary<string, int> Scoreboard;

    void Awake() {
        Scoreboard = new Dictionary<string, int>();
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
            Game game = JsonUtility.FromJson<Game>(text);
            State = (GameState)Enum.Parse(typeof(GameState), game.state.id, true);
            StateDuration = game.state.duration;
            EndTime = DateTime.Parse(game.endTime);
            Mode = (GameMode)Enum.Parse(typeof(GameMode), game.mode.id, true);
            ModeName = game.mode.name;
            for(int scoreboardIndex = 0; scoreboardIndex < game.scoreboard.Count; scoreboardIndex++) {
                Debug.Log("game.scoreboard[" + scoreboardIndex + "].id = " + game.scoreboard[scoreboardIndex].id);
                Debug.Log("game.scoreboard[" + scoreboardIndex + "].score = " + game.scoreboard[scoreboardIndex].score);
            }
        }
    }

    void Update() {
        if(EndTime < DateTime.Now) {
            FetchGameAsync();
        }
    }
}