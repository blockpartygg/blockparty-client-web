using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class LeaderboardRenderer : MonoBehaviour {
	LeaderboardManager leaderboardManager;
	public GameObject FirstPlaceScore;
	public GameObject RemainingScores;
	public GameObject EmptyLeaderboardMessage;
	public TMP_Text FirstPlacePlayerName;
	public TMP_Text FirstPlacePlayerScore;
	public GameObject LeaderboardContent;
	public GameObject LeaderboardEntryPrefab;
	public string ScoreStringFormat = "{0} <size=24>STARS</size>";
    public string FirstPlaceScoreStringFormat = "{0} <size=24>STARS</size>";

	void Awake() {
		leaderboardManager = GetComponent<LeaderboardManager>();
	}

	void Start() {
		leaderboardManager.LeaderboardUpdated += HandleLeaderboardUpdated;
		leaderboardManager.FetchLeaderboardAsync();
	}

	void HandleLeaderboardUpdated(object sender, EventArgs args) {
		if(leaderboardManager.Leaderboard.Count > 0) {
			// First clear out any existing leaderboard content
			foreach(Transform child in LeaderboardContent.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// Create a new copy of the leaderboard to sort (NOTE: this shouldn't be needed any longer because the server sorts the leaderboard)
			List<SerializableLeaderboardItem> items = new List<SerializableLeaderboardItem>(leaderboardManager.Leaderboard);
			
			// Sort in descending order
			items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

			FirstPlacePlayerName.text = items[0].id;
			FirstPlacePlayerScore.text = string.Format(FirstPlaceScoreStringFormat, items[0].score);

			items.RemoveAt(0);

			int rank = 2;

			foreach(SerializableLeaderboardItem item in items) {
				GameObject entryObject = Instantiate(LeaderboardEntryPrefab, Vector3.zero, Quaternion.identity);
				entryObject.transform.Find("Player Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
				entryObject.transform.Find("Player Name").GetComponent<TMP_Text>().text = item.id;
				entryObject.transform.Find("Player Score").GetComponent<TMP_Text>().text = item.score.ToString();
				entryObject.transform.SetParent(LeaderboardContent.transform);
				entryObject.transform.localScale = Vector3.one;
			}
		}
		else {
			FirstPlaceScore.SetActive(false);
			RemainingScores.SetActive(false);
			EmptyLeaderboardMessage.SetActive(true);
		}
	}

	void Destroy() {
		leaderboardManager.LeaderboardUpdated -= HandleLeaderboardUpdated;
	}
}
