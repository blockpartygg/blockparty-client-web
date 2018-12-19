using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class ScoreboardItem {
    public string id;
    public int score;
}

[Serializable] public class SerializableScoreboard {
    public List<ScoreboardItem> items;
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
            Scoreboard.Items = scoreboard.items;

            OnScoreboardUpdated.Raise();
        }
    }
}