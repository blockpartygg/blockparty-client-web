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

public class ScoreboardManager : MonoBehaviour {
    public List<SerializableScore> Scoreboard;
    public event EventHandler ScoreboardUpdated;

    void Awake() {
        Scoreboard = new List<SerializableScore>();
    }

    void Start() {
        FetchScoreboardAsync();
    }

    public void FetchScoreboardAsync() {
        StartCoroutine(FetchScoreboard());
    }

    IEnumerator FetchScoreboard() {
        UnityWebRequest request = UnityWebRequest.Get(APIManager.Instance.HostURL + APIManager.Instance.ScoreboardRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableScoreboard scoreboard = JsonUtility.FromJson<SerializableScoreboard>(text);
            Scoreboard = scoreboard.items;

            if(ScoreboardUpdated != null) {
                ScoreboardUpdated(this, null);
            }
        }
    }
}