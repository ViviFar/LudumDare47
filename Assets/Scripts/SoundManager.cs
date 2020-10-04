using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    public AudioSource MainSource;
    public AudioSource HighSource;
    
    public AudioClip[] loopedClip;
    public AudioClip highClip;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MainSource.clip = loopedClip[0];
        MainSource.loop = true;
        MainSource.Play();
    }

    public void ChangeMainTheme(int newTheme)
    {
        if (newTheme < loopedClip.Length)
        {
            MainSource.clip = loopedClip[newTheme];
            MainSource.loop = true;
            MainSource.Play();
            HighSource.loop = false;
            HighSource.Stop();
        }
    }

    public void launchHigh()
    {
        MainSource.clip = loopedClip [0];
        MainSource.loop = true;
        MainSource.Play();
        HighSource.clip = highClip;
        HighSource.loop = true;
        HighSource.Play();
    }
}
