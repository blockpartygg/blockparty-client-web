using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ResultsSubmitter : MonoBehaviour {
    public Game Game;
    public PlayerManager PlayerManager;
    public APIConfiguration APIConfiguration;
    public Score Score;

    public void SubmitResultsAsync() {
        if(Game.State == GameState.PostMinigame) {
            StartCoroutine(SubmitResults());
        }
    }

    IEnumerator SubmitResults() {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerManager.Name);
        form.AddField("score", Score.Points.ToString());

        UnityWebRequest request = UnityWebRequest.Post(APIConfiguration.HostURL + APIConfiguration.ResultsRoute, form);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
    }
}