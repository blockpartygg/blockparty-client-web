using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class ScoreboardRenderer : MonoBehaviour {
	ScoreboardManager scoreboardManager;
	public GameObject FirstPlaceScore;
	public GameObject RemainingScores;
	public GameObject EmptyScoreboardMessage;
	public TMP_Text FirstPlacePlayerName;
	public TMP_Text FirstPlacePlayerScore;
	public GameObject ScoreboardContent;
	public GameObject ScoreboardEntryPrefab;
	public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

	void Awake() {
		scoreboardManager = GetComponent<ScoreboardManager>();
	}

	void Start() {
		scoreboardManager.ScoreboardUpdated += HandleScoreboardUpdated;
		scoreboardManager.FetchScoreboardAsync();
	}

	void HandleScoreboardUpdated(object sender, EventArgs args) {
		if(scoreboardManager.Scoreboard.Count > 0) {
			// First clear out any existing scoreboard content
			foreach(Transform child in ScoreboardContent.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// Create a new copy of the scoreboard to sort
			List<SerializableScore> scores = new List<SerializableScore>(scoreboardManager.Scoreboard);
			
			// Sort in descending order
			scores.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

			FirstPlacePlayerName.text = scores[0].id;
			FirstPlacePlayerScore.text = string.Format(ScoreStringFormat, scores[0].score);

			scores.RemoveAt(0);

			int rank = 2;

			foreach(SerializableScore score in scores) {
				GameObject entryObject = Instantiate(ScoreboardEntryPrefab, Vector3.zero, Quaternion.identity);
				entryObject.transform.Find("Player Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
				entryObject.transform.Find("Player Name").GetComponent<TMP_Text>().text = score.id;
				entryObject.transform.Find("Player Score").GetComponent<TMP_Text>().text = score.score.ToString();
				entryObject.transform.SetParent(ScoreboardContent.transform);
				entryObject.transform.localScale = Vector3.one;
			}
		}
		else {
			FirstPlaceScore.SetActive(false);
			RemainingScores.SetActive(false);
			EmptyScoreboardMessage.SetActive(true);
		}
	}
}
