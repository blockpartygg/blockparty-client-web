using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[Serializable] 
public class SerializableGame {
    public string state;
    public string endTime;
    public int round;
    public string mode;
}

public class GameFetcher : MonoBehaviour {
    public APIConfiguration APIConfiguration;
    public Game Game;
    public GameEvent OnGameUpdated;

    void Start() {
        StartCoroutine(FetchGame());
    }

    IEnumerator FetchGame() {
        UnityWebRequest request = UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.GameRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            StartCoroutine(FetchGame());
        }
        else {
            string text = request.downloadHandler.text;
            SerializableGame game = JsonUtility.FromJson<SerializableGame>(text);
            Game.State = (GameState)Enum.Parse(typeof(GameState), game.state, true);
            Game.EndTime = DateTime.Parse(game.endTime);
            Game.Round = game.round;
            Game.Mode = !string.IsNullOrEmpty(game.mode) ? (GameMode)Enum.Parse(typeof(GameMode), game.mode, true) : GameMode.None;
            
            OnGameUpdated.Raise();
        }
    }
}