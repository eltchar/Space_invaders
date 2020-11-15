using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenager : MonoBehaviour
{
    private TextMeshProUGUI scoreCounterText;
    private TextMeshProUGUI powerUpText;
    private Button powerUpButton;
    private float powerUpCdTimer=40f;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounterText = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        powerUpText = GameObject.Find("TextPowerUp").GetComponent<TextMeshProUGUI>();
        powerUpButton = GameObject.Find("ButtonPowerUp").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounterText.text = "Points: " + GameManagerScript.instance.score.ToString();
        if (GameManagerScript.instance.enemyCount==0)
        {
            GameManagerScript.instance.MoveToScore();
        }
        if (powerUpCdTimer > 0 && !GameManagerScript.instance.powerUpEnabled)
        {
            powerUpCdTimer -= Time.deltaTime;
            powerUpText.text =((int)math.round(powerUpCdTimer)).ToString();
        }
        else if(!GameManagerScript.instance.powerUpEnabled)
        {
            powerUpText.text = "Power Up";
            powerUpButton.interactable = true;
        }
    }

    public void EnablePowerUp()
    {
        AudioManager.instance.Play("PowerUpSFX");
        GameManagerScript.instance.powerUpEnabled = true;
        powerUpCdTimer = 40f;
        powerUpButton.interactable = false;
        powerUpText.text = "Active";
    }

    public void ReturnToMenu()
    {
        GameManagerScript.instance.MoveToMainMenu();
    }
}
