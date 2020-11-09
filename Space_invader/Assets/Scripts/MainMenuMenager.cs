using TMPro;
using UnityEngine;

public class MainMenuMenager : MonoBehaviour
{
    public TextMeshProUGUI scoreRankText;
    public TextMeshProUGUI gamesCountText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreRankText.text = "";
        for (int i = 0; i < GameManagerScript.instance.rankingData.scoreRanking.Length; i++)
        {
            scoreRankText.text += GameManagerScript.instance.rankingData.scoreRanking[i].ToString() + "\n";
        }
        gamesCountText.text = "Games Played: " + GameManagerScript.instance.rankingData.gamesCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GameManagerScript.instance.MoveToLevel1();
    }

    public void UpdateRanking()
    {

    }
}
