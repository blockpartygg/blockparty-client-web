using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class SerializableLeaderboardItem {
    public string id;
    public int score;
}

[Serializable] public class SerializableLeaderboard {
    public List<SerializableLeaderboardItem> items;
}

public class LeaderboardFetcher : MonoBehaviour {
    public APIConfiguration APIConfiguration;
    public Leaderboard Leaderboard;
    public GameEvent OnLeaderboardUpdated;

    void Start() {
        FetchLeaderboardAsync();
    }

    public void FetchLeaderboardAsync() {
        StartCoroutine(FetchLeaderboard());
    }

    IEnumerator FetchLeaderboard() {
        UnityWebRequest request = UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.LeaderboardRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableLeaderboard leaderboard = JsonUtility.FromJson<SerializableLeaderboard>(text);
            Leaderboard.Scores = leaderboard.items;

            OnLeaderboardUpdated.Raise();
        }
    }
}