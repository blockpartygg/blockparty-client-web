using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class LeaderboardItem {
    public string id;
    public int score;
}

[Serializable] public class SerializableLeaderboards {
    public List<LeaderboardItem> timeAttackItems;
    public List<LeaderboardItem> survivalItems;
}

public class LeaderboardsFetcher : MonoBehaviour {
    public APIConfiguration APIConfiguration;
    public Leaderboards Leaderboards;
    public GameEvent OnLeaderboardsUpdated;

    void Start() {
        FetchLeaderboardsAsync();
    }

    public void FetchLeaderboardsAsync() {
        StartCoroutine(FetchLeaderboards());
    }

    IEnumerator FetchLeaderboards() {
        UnityWebRequest request= UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.LeaderboardsRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            StartCoroutine(FetchLeaderboards());
        }
        else {
            string text = request.downloadHandler.text;
            SerializableLeaderboards leaderboards = JsonUtility.FromJson<SerializableLeaderboards>(text);
            Leaderboards.TimeAttackLeaderboardItems = leaderboards.timeAttackItems;
            Leaderboards.SurvivalLeaderboardItems = leaderboards.survivalItems;

            OnLeaderboardsUpdated.Raise();
        }
    }
}