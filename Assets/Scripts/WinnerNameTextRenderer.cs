using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WinnerNameTextRenderer : MonoBehaviour {
    public Scoreboard Scoreboard;
    public TMP_Text Text;
    
    public void HandleScoreboardUpdated() {
        if(Scoreboard.Items.Count > 0) {
            // Create a new copy of the scoreboard to sort (NOTE: this shouldn't be needed any longer because the server sorts the scoreboard)
			List<ScoreboardItem> items = new List<ScoreboardItem>(Scoreboard.Items);
			
			// Sort in descending order
			items.Sort((firstItem, secondItem) => -1 * firstItem.score.CompareTo(secondItem.score));

            Text.text = items[0].id;
        }
    }
}