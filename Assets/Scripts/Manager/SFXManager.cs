using UnityEngine;
using System.Collections;

public enum sound
{
    explosion,
    hit,
    powerup,
    laser
}

public class SFXManager : MonoBehaviour
{

    public static SFXManager instance;


    public AudioClip explosion, hit, powerup, laser;
    private AudioSource sfx;

    void Awake()
    {
        instance = this;

    }

    // Use this for initialization
    void Start()
    {
        sfx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSFX(sound s)
    {

        switch (s)
        {
            case sound.explosion:
                sfx.PlayOneShot(explosion);
                break;
            case sound.hit:
                sfx.PlayOneShot(hit);
                break;
            case sound.laser:
                sfx.PlayOneShot(laser);
                break;
            case sound.powerup:
                sfx.PlayOneShot(powerup);
                break;
        }
    }

    public void setVolume(float v)
    {
        sfx.volume = v;
    }
}
