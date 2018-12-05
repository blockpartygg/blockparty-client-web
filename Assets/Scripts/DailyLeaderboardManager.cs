using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class SerializableDailyLeaderboardItem {
    public string id;
    public int score;
}

[Serializable] public class SerializableDailyLeaderboard {
    public List<SerializableDailyLeaderboardItem> timeAttackItems;
    public List<SerializableDailyLeaderboardItem> survivalItems;
}

public class DailyLeaderboardManager : MonoBehaviour {
    public List<SerializableDailyLeaderboardItem> TimeAttackLeaderboard;
    public List<SerializableDailyLeaderboardItem> SurvivalLeaderboard;
    public event EventHandler LeaderboardUpdated;

    void Awake() {
        TimeAttackLeaderboard = new List<SerializableDailyLeaderboardItem>();
        SurvivalLeaderboard = new List<SerializableDailyLeaderboardItem>();
    }

    void Start() {
        FetchDailyLeaderboardAsync();
    }

    public void FetchDailyLeaderboardAsync() {
        StartCoroutine(FetchDailyLeaderboard());
    }

    IEnumerator FetchDailyLeaderboard() {
        UnityWebRequest request= UnityWebRequest.Get(APIManager.Instance.HostURL + APIManager.Instance.DailyLeaderboardRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableDailyLeaderboard leaderboard = JsonUtility.FromJson<SerializableDailyLeaderboard>(text);
            TimeAttackLeaderboard = leaderboard.timeAttackItems;
            SurvivalLeaderboard = leaderboard.survivalItems;

            if(LeaderboardUpdated != null) {
                LeaderboardUpdated(this, null);
            }
        }
    }
}