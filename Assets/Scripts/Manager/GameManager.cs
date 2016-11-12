using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject player1, player2;
    public float sfxVolume, bgmVolume;
    public Slider sfxSlider, bgmSlider;
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

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
