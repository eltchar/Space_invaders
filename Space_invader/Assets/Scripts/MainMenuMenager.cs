using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMenager : MonoBehaviour
{
    public TextMeshProUGUI scoreRankText;
    public TextMeshProUGUI gamesCountText;
    private Slider sliderBGM;
    private Slider sliderSFX;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        sliderBGM = GameObject.Find("Slider_BGM").GetComponent<Slider>();
        sliderBGM.value = AudioManager.instance.BGMVolume;
        sliderSFX = GameObject.Find("Slider_SFX").GetComponent<Slider>();
        sliderSFX.value = AudioManager.instance.SFXVolume;
        GameObject.Find("OptionsMenu").SetActive(false);
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

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.instance.ChangeSFXVolume(volume);
    }
    public void ChangeBGMVolume(float volume)
    {
        AudioManager.instance.ChangeBGMVolume(volume);
    }
}
