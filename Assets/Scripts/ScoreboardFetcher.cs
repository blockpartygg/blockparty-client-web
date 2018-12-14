using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class SerializableScore {
    public string id;
    public int score;
}

[Serializable] public class SerializableScoreboard {
    public List<SerializableScore> items;
}

public class ScoreboardFetcher : MonoBehaviour {
    public APIConfiguration APIConfiguration;
    public Scoreboard Scoreboard;
    public GameEvent OnScoreboardUpdated;

    void Start() {
        FetchScoreboardAsync();
    }

    public void FetchScoreboardAsync() {
        StartCoroutine(FetchScoreboard());
    }

    IEnumerator FetchScoreboard() {
        UnityWebRequest request = UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.ScoreboardRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableScoreboard scoreboard = JsonUtility.FromJson<SerializableScoreboard>(text);
            Scoreboard.Scores = scoreboard.items;

            OnScoreboardUpdated.Raise();
        }
    }
}