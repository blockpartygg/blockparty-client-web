using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class ScoreboardRenderer : MonoBehaviour {
	public Scoreboard Scoreboard;
	public GameObject ScoresGameObject;
	public GameObject EmptyScoreboardMessage;
	public GameObject ScoreboardContent;
	public GameObject ScoreboardEntryPrefab;
	public string ScoreStringFormat = "{0} <size=24>STARS</size>";

	public void HandleScoreboardUpdated() {
		if(Scoreboard.Items.Count > 0) {
			// First clear out any existing scoreboard content
			foreach(Transform child in ScoreboardContent.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// Create a new copy of the scoreboard to sort (NOTE: this shouldn't be needed any longer because the server sorts the scoreboard)
			List<ScoreboardItem> items = new List<ScoreboardItem>(Scoreboard.Items);
			
			// Sort in descending order
			items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

			int rank = 1;

			foreach(ScoreboardItem item in items) {
				GameObject entryObject = Instantiate(ScoreboardEntryPrefab, Vector3.zero, Quaternion.identity);
				entryObject.transform.Find("Player Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
				entryObject.transform.Find("Player Name").GetComponent<TMP_Text>().text = item.id;
				entryObject.transform.Find("Player Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score);
				entryObject.transform.SetParent(ScoreboardContent.transform);
				entryObject.transform.localScale = Vector3.one;
			}
		}
		else {
			ScoresGameObject.SetActive(false);
			EmptyScoreboardMessage.SetActive(true);
		}
	}
}
