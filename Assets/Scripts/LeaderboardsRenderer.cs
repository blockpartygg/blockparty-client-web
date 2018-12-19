using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class LeaderboardsRenderer : MonoBehaviour {
    public Leaderboards Leaderboards;
    public GameObject TimeAttackLeaderboardContent;
    public GameObject SurvivalLeaderboardContent;
    public GameObject LeaderboardEntryPrefab;
    public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

    public void HandleLeaderboardsUpdated() {
        RenderLeaderboard(Leaderboards.TimeAttackLeaderboardItems, TimeAttackLeaderboardContent);
        RenderLeaderboard(Leaderboards.SurvivalLeaderboardItems, SurvivalLeaderboardContent);
    }

    void RenderLeaderboard(List<LeaderboardItem> leaderboardItems, GameObject content) {
        if(leaderboardItems.Count > 0) {
            // First clear out any existing content
            foreach(Transform child in content.transform) {
                GameObject.Destroy(child.gameObject);
            }

            // Create a new copy of the leaderboard to sort
            List<LeaderboardItem> itemsCopy = new List<LeaderboardItem>(leaderboardItems);

            // Sort in descending order
            itemsCopy.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

            int rank = 1;

            foreach(LeaderboardItem item in itemsCopy) {
                GameObject entryObject = Instantiate(LeaderboardEntryPrefab);
                entryObject.transform.Find("Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
                entryObject.transform.Find("Name").GetComponent<TMP_Text>().text = item.id;
                entryObject.transform.Find("Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score);
                entryObject.transform.SetParent(content.transform);
                entryObject.transform.localScale = Vector3.one;
            }
        }
    }
}