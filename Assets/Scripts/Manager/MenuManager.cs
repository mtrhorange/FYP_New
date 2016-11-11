using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public Slider sfxSlider;
    public Slider bgmSlider;
    public float bgmValue = 1;
    public float sfxValue = 1;


    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void setSFX()
    {
        sfxValue = sfxSlider.value;
    }

    public void setBGM()
    {
        bgmValue = bgmSlider.value;
    }
}
