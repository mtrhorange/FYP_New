using UnityEngine;
using System.Collections;
public class BGMManager : MonoBehaviour
{

    public static BGMManager instance;

    public float bgmValue = 1;
    public float sfxValue = 1;

    void Awake()
    {
        if (GameObject.Find("BGMManager") != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
	// Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVolume(float v)
    {
        GetComponent<AudioSource>().volume = v;
    }

    public void setSFX(float f)
    {
        sfxValue = f;
    }

    public void setBGM(float f)
    {
        bgmValue = f;
        setVolume(f);
    }
}
