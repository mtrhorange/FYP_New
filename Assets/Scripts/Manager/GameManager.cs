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

	public bool isGameOver = false;
	public bool isPaused = false;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start ()
	{
	    sfxVolume = MenuManager.instance.sfxValue;
	    bgmVolume = MenuManager.instance.bgmValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setSFX()
    {
        sfxVolume = sfxSlider.value;
    }

    public void setBGM()
    {
        bgmVolume = bgmSlider.value;
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

		isGameOver = true;
	}

	public void loadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
