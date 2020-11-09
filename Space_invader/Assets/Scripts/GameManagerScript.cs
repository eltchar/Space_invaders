using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance { get; private set; }
    public int entityDirection = 1;
    public float difficultyModifier = 0f;
    public int score = 0;
    public int[] collumnCount = new int[8];
    public int enemyCount = 32;
    public RankingData rankingData;
    private string pathToScores;

    private void Awake()
    {
        //Setting up singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // loading up scores
            pathToScores = Application.persistentDataPath + "/data.bin";
            if (File.Exists(pathToScores))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(pathToScores, FileMode.Open);
                rankingData = formatter.Deserialize(stream) as RankingData;
                stream.Close();
            }
            else
            {
                int[] arr = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    arr[i] = 0;
                }
                rankingData = new RankingData(arr, 0);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetLevel1()
    {
        for (int i = 0; i < collumnCount.Length; i++)
        {
            collumnCount[i] = 4;
        }
        difficultyModifier = 0f;
        score = 0;
        entityDirection = 1;
        enemyCount = 32;
    }

    public void MoveToLevel1()
    {
        ResetLevel1();
        SceneManager.LoadScene("Level1");
    }

    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToScore()
    {
        SceneManager.LoadScene("ScoreScreen");
    }

    public int CheckScoreRank()
    {
        rankingData.gamesCount += 1;
        for (int i = 0; i < rankingData.scoreRanking.Length; i++)
        {
            if (rankingData.scoreRanking[i]<score)
            {
                for (int j = rankingData.scoreRanking.Length-1; j > i; j--)
                {
                    rankingData.scoreRanking[j] = rankingData.scoreRanking[j-1];
                }
                rankingData.scoreRanking[i] = score;
                SaveScoreRanking();
                return (i+1);
            }
        }
        SaveScoreRanking();
        return 0;
    }

    public void SaveScoreRanking()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathToScores, FileMode.Create);
        formatter.Serialize(stream, rankingData);
        stream.Close();
    }
    
}
