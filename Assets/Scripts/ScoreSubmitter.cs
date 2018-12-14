using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScoreSubmitter : MonoBehaviour {
    public Game Game;
    public PlayerManager PlayerManager;
    public APIConfiguration APIConfiguration;
    public Score Score;

    public void SubmitScoreAsync() {
        if(Game.State == GameState.PostMinigame) {
            StartCoroutine(SubmitScore());
        }
    }

    IEnumerator SubmitScore() {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerManager.Name);
        form.AddField("score", Score.Points.ToString());

        UnityWebRequest request = UnityWebRequest.Post(APIConfiguration.HostURL + APIConfiguration.ScoreRoute, form);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
    }
}