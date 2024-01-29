using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreEntry : MonoBehaviour
{

    public TextMeshProUGUI entryName;
    public TextMeshProUGUI entryScore;

    public void UpdateEntry(string playerName, int playerScore)
    {
        if (playerName != null)
        {
            entryName.text = playerName;
        }
        if (playerScore != null)
        {
            entryScore.text = playerScore.ToString();
        }
    }

}
