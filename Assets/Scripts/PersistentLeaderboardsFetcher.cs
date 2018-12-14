using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class SerializablePersistentLeaderboardItem {
    public string id;
    public int score;
}

[Serializable] public class SerializablePersistentLeaderboards {
    public List<SerializablePersistentLeaderboardItem> timeAttackItems;
    public List<SerializablePersistentLeaderboardItem> survivalItems;
}

public class PersistentLeaderboardsFetcher : MonoBehaviour {
    public PersistentLeaderboards PersistentLeaderboards;
    public APIConfiguration APIConfiguration;

    void Start() {
        FetchPersistentLeaderboardsAsync();
    }

    public void FetchPersistentLeaderboardsAsync() {
        StartCoroutine(FetchPersistentLeaderboards());
    }

    IEnumerator FetchPersistentLeaderboards() {
        UnityWebRequest request= UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.PersistentLeaderboardsRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            StartCoroutine(FetchPersistentLeaderboards());
        }
        else {
            string text = request.downloadHandler.text;
            SerializablePersistentLeaderboards leaderboard = JsonUtility.FromJson<SerializablePersistentLeaderboards>(text);
            PersistentLeaderboards.TimeAttackLeaderboard = leaderboard.timeAttackItems;
            PersistentLeaderboards.SurvivalLeaderboard = leaderboard.survivalItems;

            PersistentLeaderboards.OnUpdated.Raise();
        }
    }
}