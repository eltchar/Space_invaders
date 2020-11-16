using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenager : MonoBehaviour
{
    private Image[] livesImage = new Image[3];
    private TextMeshProUGUI scoreCounterText;
    private TextMeshProUGUI powerUpText;
    private Button powerUpButton;
    private float powerUpCdTimer=40f;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.instance.OnUiUpdateEvent += UiUpdate;
        livesImage[0] = GameObject.Find("LivesIcon1").GetComponent<Image>();
        livesImage[1] = GameObject.Find("LivesIcon2").GetComponent<Image>();
        livesImage[2] = GameObject.Find("LivesIcon3").GetComponent<Image>();
        scoreCounterText = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        powerUpText = GameObject.Find("TextPowerUp").GetComponent<TextMeshProUGUI>();
        powerUpButton = GameObject.Find("ButtonPowerUp").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.enemyCount==0)
        {
            GameManagerScript.instance.MoveToScore();
        }
        if (GameManagerScript.instance.liveCount == 0)
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

    private void UiUpdate(object sender, EventArgs e)
    {
        UpdateLives();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreCounterText.text = "Points: " + GameManagerScript.instance.score.ToString();
    }

    //depending on life count update graphics
    private void UpdateLives()
    {
        switch (GameManagerScript.instance.liveCount)
        {
            case 3:
                livesImage[2].gameObject.SetActive(true);
                livesImage[1].gameObject.SetActive(true);
                livesImage[0].gameObject.SetActive(true);
                break;
            case 2:
                livesImage[2].gameObject.SetActive(false);
                livesImage[1].gameObject.SetActive(true);
                livesImage[0].gameObject.SetActive(true);
                break;
            case 1:
                livesImage[2].gameObject.SetActive(false);
                livesImage[1].gameObject.SetActive(false);
                livesImage[0].gameObject.SetActive(true);
                break;
            case 0:
                livesImage[2].gameObject.SetActive(false);
                livesImage[1].gameObject.SetActive(false);
                livesImage[0].gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    private void OnDestroy()
    {
        GameManagerScript.instance.OnUiUpdateEvent -= UiUpdate;
    }
}
