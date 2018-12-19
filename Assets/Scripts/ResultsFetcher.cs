using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] public class ResultsItem {
    public string id;
    public int score;
}

[Serializable] public class SerializableResults {
    public List<ResultsItem> items;
}

public class ResultsFetcher : MonoBehaviour {
    public APIConfiguration APIConfiguration;
    public Results Results;
    public GameEvent OnResultsUpdated;

    void Start() {
        FetchResultsAsync();
    }

    public void FetchResultsAsync() {
        StartCoroutine(FetchResults());
    }

    IEnumerator FetchResults() {
        UnityWebRequest request = UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.ResultsRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableResults results = JsonUtility.FromJson<SerializableResults>(text);
            Results.Items = results.items;

            OnResultsUpdated.Raise();
        }
    }
}