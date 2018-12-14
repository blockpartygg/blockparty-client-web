using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class PersistentLeaderboardsRenderer : MonoBehaviour {
    public PersistentLeaderboards PersistentLeaderboards;
    public GameObject TimeAttackLeaderboardContent;
    public GameObject SurvivalLeaderboardContent;
    public GameObject LeaderboardEntryPrefab;
    public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

    public void HandleLeaderboardsUpdated() {
        Debug.Log("Updating leaderboard");
        if(PersistentLeaderboards.TimeAttackLeaderboard.Count > 0) {
            // First clear out any existing leaderboard content
            foreach(Transform child in TimeAttackLeaderboardContent.transform) {
                GameObject.Destroy(child.gameObject);
            }

            // Create a new copy of the leaderboard to sort
            List<SerializablePersistentLeaderboardItem> items = new List<SerializablePersistentLeaderboardItem>(PersistentLeaderboards.TimeAttackLeaderboard);

            // Sort in descending order
            items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

            int rank = 1;

            foreach(SerializablePersistentLeaderboardItem item in items) {
                GameObject entryObject = Instantiate(LeaderboardEntryPrefab, Vector3.zero, Quaternion.identity);
                entryObject.transform.Find("Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
                entryObject.transform.Find("Name").GetComponent<TMP_Text>().text = item.id;
                entryObject.transform.Find("Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score.ToString());
                entryObject.transform.SetParent(TimeAttackLeaderboardContent.transform);
                entryObject.transform.localScale = Vector3.one;
            }
        }

        if(PersistentLeaderboards.SurvivalLeaderboard.Count > 0) {
            // First clear out any existing leaderboard content
            foreach(Transform child in SurvivalLeaderboardContent.transform) {
                GameObject.Destroy(child.gameObject);
            }

            // Create a new copy of the leaderboard to sort
            List<SerializablePersistentLeaderboardItem> items = new List<SerializablePersistentLeaderboardItem>(PersistentLeaderboards.SurvivalLeaderboard);

            // Sort in descending order
            items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

            int rank = 1;

            foreach(SerializablePersistentLeaderboardItem item in items) {
                GameObject entryObject = Instantiate(LeaderboardEntryPrefab, Vector3.zero, Quaternion.identity);
                entryObject.transform.Find("Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
                entryObject.transform.Find("Name").GetComponent<TMP_Text>().text = item.id;
                entryObject.transform.Find("Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score.ToString());
                entryObject.transform.SetParent(SurvivalLeaderboardContent.transform);
                entryObject.transform.localScale = Vector3.one;
            }
        }
    }
}