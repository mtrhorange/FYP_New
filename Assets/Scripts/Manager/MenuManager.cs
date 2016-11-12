using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{


    public Slider sfxSlider;
    public Slider bgmSlider;

    // Use this for initialization
    void Start ()
    {
        if (BGMManager.instance)
        {
            sfxSlider.value = BGMManager.instance.sfxValue;
            bgmSlider.value = BGMManager.instance.bgmValue;

        }

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

    public void setSFXSlider()
    {
        BGMManager.instance.setSFX(sfxSlider.value);
    }

    public void setBGMSlider()
    {
        BGMManager.instance.setBGM(bgmSlider.value);
    }
}
