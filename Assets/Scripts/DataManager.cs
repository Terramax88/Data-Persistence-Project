using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instate;

    [SerializeField]
    Text bestScoreText;
    [SerializeField]
    Text playerNameText;
    
    public PlayerData bestPlayerScore;
    public string textBestScore;

    private void Awake()
    {
        if(Instate != null)
        {
            Destroy(gameObject);
        }
        Instate = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadBestScore();
        textBestScore = "Best Score : " + bestPlayerScore.bestPlayer + " : " + bestPlayerScore.bestScore;        
        bestScoreText.text = textBestScore;

        playerNameText.text = bestPlayerScore.playerName;
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void InputPlayerName(string nameText)
    {
        bestPlayerScore.playerName = nameText;
    }

    public void ScoreToBest(int score)
    {
        if(score > bestPlayerScore.bestScore)
        {
            bestPlayerScore.bestPlayer = bestPlayerScore.playerName;
            bestPlayerScore.bestScore = score;
            textBestScore = "Best Score : " + bestPlayerScore.bestPlayer + " : " + bestPlayerScore.bestScore;
        }
    }




    [System.Serializable]
    public class PlayerData
    {
        public string bestPlayer = "Player";
        public int bestScore = 0;
        public string playerName = "Enter Name";
    }

    

    public void SaveBestScore()
    {
        string savePath = Application.persistentDataPath + "/saveBestScore.json";
        string json = JsonUtility.ToJson(bestPlayerScore);
        File.WriteAllText(savePath, json);
    }

    public void LoadBestScore()
    {
        string savePath = Application.persistentDataPath + "/saveBestScore.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            bestPlayerScore = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            bestPlayerScore = new PlayerData();
        }
    }

    private void OnDestroy()
    {
        SaveBestScore();
    }
}
