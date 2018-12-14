using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class ScoreboardRenderer : MonoBehaviour {
	public Scoreboard Scoreboard;
	public GameObject FirstPlaceScore;
	public GameObject RemainingScores;
	public GameObject EmptyScoreboardMessage;
	public TMP_Text FirstPlacePlayerName;
	public TMP_Text FirstPlacePlayerScore;
	public GameObject ScoreboardContent;
	public GameObject ScoreboardEntryPrefab;
	public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

	public void HandleScoreboardUpdated() {
		if(Scoreboard.Scores.Count > 0) {
			// First clear out any existing scoreboard content
			foreach(Transform child in ScoreboardContent.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// Create a new copy of the scoreboard to sort
			List<SerializableScore> scores = new List<SerializableScore>(Scoreboard.Scores);
			
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
				entryObject.transform.Find("Player Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, 0);
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
