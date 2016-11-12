using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player1, player2;
    public float sfxVolume, bgmVolume;
    public Slider sfxSlider, bgmSlider;
    public GameObject gameOverPanel;

	public bool isGameOver = false;
	public bool isPaused = false;

    void Awake()
    {
      
        instance = this;
    }
	// Use this for initialization
	void Start ()
	{
	    if (BGMManager.instance)
	    {
	        sfxVolume = BGMManager.instance.sfxValue;
            sfxSlider.value = BGMManager.instance.sfxValue;
            bgmVolume = BGMManager.instance.bgmValue;
            bgmSlider.value = BGMManager.instance.bgmValue;
            SFXManager.instance.setVolume(sfxVolume);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setSFX()
    {
        sfxVolume = sfxSlider.value;
        BGMManager.instance.setSFX(sfxVolume);
        SFXManager.instance.setVolume(sfxVolume);
    }

    public void setBGM()
    {
        bgmVolume = bgmSlider.value;
        BGMManager.instance.setBGM(bgmVolume);
    }

	public void PauseGame() {

		Time.timeScale = 0;
		isPaused = true;
	}

	public void ResumeGame() {
		Time.timeScale = 1.0f;
		isPaused = false;
	}

	public void GameOver() {

        Time.timeScale = 0;
        isGameOver = true;
        gameOverPanel.SetActive(true);

	}

	public void loadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
