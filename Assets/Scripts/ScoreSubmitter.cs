using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScoreSubmitter : MonoBehaviour {
    public Score Score;

    public void SubmitScoreAsync() {
        StartCoroutine(SubmitScore());
    }

    IEnumerator SubmitScore() {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerManager.Instance.Name);
        form.AddField("score", Score.Points.ToString());

        UnityWebRequest request = UnityWebRequest.Post("http://localhost:1337/score", form);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            Debug.Log("Sent HTTP POST");
        }
    }
}