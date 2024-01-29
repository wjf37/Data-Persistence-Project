using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersist : MonoBehaviour
{
    public static DataPersist Instance;
    public string nameInput;
    public string score;
    public TextMeshProUGUI bestScore;
    private readonly int maxEntries = 7;
    private Entry bestScoreEntry = new();

    [System.Serializable]
    public class Entry{
        public string entryName;
        public int entryPoints;
    } 

    [System.Serializable]
    public class LeaderboardList
    {
        public List<Entry> leaderboardSer;

        public LeaderboardList(List<Entry> entries)
        {
            leaderboardSer = entries;
        }


    }
    private void Awake()
    {
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            InitLeaderboard();
        }
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            bestScoreEntry = DisplayBestScore();
            bestScore.text = "Best Score : " + bestScoreEntry.entryName + " : " + bestScoreEntry.entryPoints.ToString();

        }
    }

    private void InitLeaderboard()
    {
        List<Entry> intLeaderboard = new();
        for (int i=0;i<maxEntries; i++)
        {
            Entry emptyData = new()
            {
                entryName = "Name",
                entryPoints = 0
            };
            intLeaderboard.Add(emptyData);
        }
        LeaderboardList wrappedList = new LeaderboardList(intLeaderboard);
        string json = JsonUtility.ToJson(wrappedList);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void AddEntry(string playerName, int score)
    {
        List<Entry> leaderboard = new();
        Entry newEntry = new()
        {
            entryName = playerName,
            entryPoints = score
        };

        leaderboard = LoadLeaderboard();
        leaderboard.Add(newEntry);
        leaderboard.Sort((a, b) => b.entryPoints.CompareTo(a.entryPoints));
        leaderboard = leaderboard.GetRange(0, 7);
        SaveLeaderboard(leaderboard);
    }

    public List<Entry> LoadLeaderboard()
    {
        List<Entry> loadLeaderboard = new();
        string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        List<Entry> midLeaderboard = JsonUtility.FromJson<LeaderboardList>(json).leaderboardSer;
        foreach (Entry entry in midLeaderboard)
        {
            loadLeaderboard.Add(entry);
        }
        return loadLeaderboard;
    }

    public void SaveLeaderboard(List<Entry> leaderboard)
    {
        LeaderboardList wrappedList = new(leaderboard);
        string json = JsonUtility.ToJson(wrappedList);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public Entry DisplayBestScore(){
        List<Entry> intLeaderboard = LoadLeaderboard();
        return intLeaderboard[0];
    }
}
