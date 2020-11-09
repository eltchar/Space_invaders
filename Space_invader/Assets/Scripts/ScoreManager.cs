using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreCounterText;
    private TextMeshProUGUI scoreRankText;
    // Start is called before the first frame update
    void Start()
    {
        scoreCounterText = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        scoreRankText = GameObject.Find("TextRank").GetComponent<TextMeshProUGUI>();
        scoreCounterText.text = "Your score: " + GameManagerScript.instance.score.ToString();
        int scoreRank = GameManagerScript.instance.CheckScoreRank();
        if (scoreRank > 0)
        {
            scoreRankText.text = "New Record! You are:  " + scoreRank.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        GameManagerScript.instance.MoveToMainMenu();
    }
}
