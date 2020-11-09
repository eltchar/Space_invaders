using TMPro;
using UnityEngine;

public class LevelMenager : MonoBehaviour
{
    private TextMeshProUGUI scoreCounterText;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounterText = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounterText.text = "Points: " + GameManagerScript.instance.score.ToString();
        if (GameManagerScript.instance.enemyCount==0)
        {
            GameManagerScript.instance.MoveToScore();
        }
    }
}
