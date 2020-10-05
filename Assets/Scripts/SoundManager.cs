using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    public AudioSource MainSource;
    public AudioSource HighSource;
    public AudioSource Narator;


    public AudioClip[] loopedClip;
    public AudioClip highClip;

    public AudioClip IntroFull, IntroReduced;
    public AudioClip[] nectars;
    public AudioClip[] nvx;

    public AudioClip conclusion;

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

    public void PlayNarator(AudioClip clip, GameStates nextState)
    {
        StartCoroutine(PlayClip(clip, nextState));
    }

    public void PlayNarator(AudioClip[] clips, GameStates nextState)
    {
        StartCoroutine(PlayClips(clips, nextState));
    }


    private IEnumerator PlayClip(AudioClip clip, GameStates nextState)
    {
        SoundManager.Instance.MainSource.volume = 0.1f;
        Narator.clip = clip;
        Narator.loop = false;
        Narator.Play();
        yield return new WaitForSeconds(Narator.clip.length);
        SoundManager.Instance.MainSource.volume = 0.4f;
        StateMachine.Instance.currentState = nextState;
    }
    private IEnumerator PlayClips(AudioClip[] clips, GameStates nextState)
    {
        SoundManager.Instance.MainSource.volume = 0.1f;
        foreach (AudioClip clip in clips)
        {
            Narator.clip = clip;
            Narator.loop = false;
            Narator.Play();
            yield return new WaitForSeconds(Narator.clip.length);
        }
        SoundManager.Instance.MainSource.volume = 0.4f;
        StateMachine.Instance.currentState = nextState;
    }

    public void StopNar(AudioClip clip, GameStates nextState)
    {
        Narator.Stop();
        SoundManager.Instance.MainSource.volume = 0.4f;
        StopCoroutine(PlayClip(clip, nextState));
        StateMachine.Instance.currentState = nextState;
    }

    public void StopNar(AudioClip[] clips, GameStates nextState)
    {
        Narator.Stop();
        SoundManager.Instance.MainSource.volume = 0.4f;
        StopCoroutine(PlayClips(clips, nextState));
        StateMachine.Instance.currentState = nextState;
    }

}
