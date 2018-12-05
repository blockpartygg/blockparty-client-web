using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class DailyLeaderboardRenderer : MonoBehaviour {
    DailyLeaderboardManager leaderboardManager;
    public GameObject LeaderboardContent;
    public GameObject LeaderboardEntryPrefab;
    public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

    void Awake() {
        leaderboardManager = GetComponent<DailyLeaderboardManager>();
    }

    void Start() {
        leaderboardManager.LeaderboardUpdated += HandleLeaderboardUpdated;
        leaderboardManager.FetchDailyLeaderboardAsync();
    }

    void HandleLeaderboardUpdated(object sender, EventArgs args) {
        if(leaderboardManager.TimeAttackLeaderboard.Count > 0) {
            // First clear out any existing leaderboard content
            foreach(Transform child in LeaderboardContent.transform) {
                GameObject.Destroy(child.gameObject);
            }

            // Create a new copy of the leaderboard to sort
            List<SerializableDailyLeaderboardItem> items = new List<SerializableDailyLeaderboardItem>(leaderboardManager.TimeAttackLeaderboard);

            // Sort in descending order
            items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

            int rank = 1;

            foreach(SerializableDailyLeaderboardItem item in items) {
                GameObject entryObject = Instantiate(LeaderboardEntryPrefab, Vector3.zero, Quaternion.identity);
                entryObject.transform.Find("Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
                entryObject.transform.Find("Name").GetComponent<TMP_Text>().text = item.id;
                entryObject.transform.Find("Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score.ToString());
                entryObject.transform.SetParent(LeaderboardContent.transform);
                entryObject.transform.localScale = Vector3.one;
            }
        }
    }

    void OnDestroy() {
        leaderboardManager.LeaderboardUpdated -= HandleLeaderboardUpdated;
    }
}