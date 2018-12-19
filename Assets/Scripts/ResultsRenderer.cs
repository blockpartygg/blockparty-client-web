using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class ResultsRenderer : MonoBehaviour {
	public Results Results;
	public GameObject ResultsGameObject;
	public GameObject EmptyResultsMessage;
	public GameObject ResultsContent;
	public GameObject ResultsEntryPrefab;
	public string ScoreStringFormat = "{0} <size=24>POINTS</size>";

	public void HandleResultsUpdated() {
		if(Results.Items.Count > 0) {
			// First clear out any existing results content
			foreach(Transform child in ResultsContent.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// Create a new copy of the results to sort
			List<ResultsItem> items = new List<ResultsItem>(Results.Items);
			
			// Sort in descending order
			items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

			int rank = 1;
			
			foreach(ResultsItem item in items) {
				GameObject entryObject = Instantiate(ResultsEntryPrefab);
				entryObject.transform.Find("Player Rank").GetComponent<TMP_Text>().text = (rank++).ToString();
				entryObject.transform.Find("Player Name").GetComponent<TMP_Text>().text = item.id;
				entryObject.transform.Find("Player Score").GetComponent<TMP_Text>().text = string.Format(ScoreStringFormat, item.score);
				entryObject.transform.SetParent(ResultsContent.transform);
				entryObject.transform.localScale = Vector3.one;
			}
		}
		else {
			ResultsGameObject.SetActive(false);
			EmptyResultsMessage.SetActive(true);
		}
	}
}
