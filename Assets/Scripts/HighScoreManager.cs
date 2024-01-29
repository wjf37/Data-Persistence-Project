using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;    
    public GameObject[] hsEntries;
    private List<DataPersist.Entry> leaderboard = new();
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UpdateLeaderboard();
    }

    public void UpdateLeaderboard(){
        leaderboard = DataPersist.Instance.LoadLeaderboard();
        int i = 0;
        foreach(GameObject entry in hsEntries){
            HighScoreEntry entryScript = entry.GetComponent<HighScoreEntry>();
            entryScript.UpdateEntry(leaderboard[i].entryName, leaderboard[i].entryPoints);
            i++;
        }
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
        Destroy(Instance);
        Destroy(DataPersist.Instance);
    }

}
