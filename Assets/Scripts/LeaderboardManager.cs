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

public class LeaderboardManager : MonoBehaviour {
    public List<SerializableLeaderboardItem> Leaderboard;
    public event EventHandler LeaderboardUpdated;

    void Awake() {
        Leaderboard = new List<SerializableLeaderboardItem>();
    }

    void Start() {
        FetchLeaderboardAsync();
    }

    public void FetchLeaderboardAsync() {
        StartCoroutine(FetchLeaderboard());
    }

    IEnumerator FetchLeaderboard() {
        UnityWebRequest request = UnityWebRequest.Get(APIManager.Instance.HostURL + APIManager.Instance.LeaderboardRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableLeaderboard leaderboard = JsonUtility.FromJson<SerializableLeaderboard>(text);
            Leaderboard = leaderboard.items;

            if(LeaderboardUpdated != null) {
                LeaderboardUpdated(this, null);
            }
        }
    }
}