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

    public void PlayNarator(AudioClip clip)
    {
        StartCoroutine(PlayClip(clip));
    }

    public void PlayNarator(AudioClip[] clips)
    {
        StartCoroutine(PlayClips(clips));
    }


    private IEnumerator PlayClip(AudioClip clip)
    {
        SoundManager.Instance.MainSource.volume = 0.1f;
        Narator.clip = clip;
        Narator.loop = false;
        Narator.Play();
        yield return new WaitForSeconds(Narator.clip.length);
        SoundManager.Instance.MainSource.volume = 0.5f;
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.MainSource.volume = 0.8f;
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.MainSource.volume = 1;
        StateMachine.Instance.currentState = GameStates.Playing;
    }
    private IEnumerator PlayClips(AudioClip[] clips)
    {
        SoundManager.Instance.MainSource.volume = 0.1f;
        foreach (AudioClip clip in clips)
        {
            Narator.clip = clip;
            Narator.loop = false;
            Narator.Play();
            yield return new WaitForSeconds(Narator.clip.length);
        }
        SoundManager.Instance.MainSource.volume = 0.5f;
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.MainSource.volume = 0.8f;
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.MainSource.volume = 1;
        StateMachine.Instance.currentState = GameStates.Playing;
    }

    //private IEnumerator PlayClips()
    //{
    //    Narator.clip = nvx[(StateMachine.Instance.CurrentLevel % 6) - 1];
    //    Narator.Play();
    //    yield return new WaitForSeconds(Narator.clip.length);
    //    Narator.clip = nectars[StateMachine.Instance.NumberOfBonusUsed / 2];
    //    Narator.Play();
    //    yield return new WaitForSeconds(Narator.clip.length);
    //    SoundManager.Instance.MainSource.volume = 0.5f;
    //    yield return new WaitForSeconds(0.5f);
    //    SoundManager.Instance.MainSource.volume = 0.8f;
    //    yield return new WaitForSeconds(0.5f);
    //    SoundManager.Instance.MainSource.volume = 1;
    //    StateMachine.Instance.currentState = GameStates.Playing;
    //}

    //private IEnumerator PlayIntro()
    //{
    //    if (StateMachine.Instance.IntroductionPlayed)
    //    {
    //        Narator.clip = IntroReduced;
    //    }
    //    else
    //    {
    //        Narator.clip = IntroFull;
    //    }
    //    Narator.loop = false;
    //    Narator.Play();
    //    yield return new WaitForSeconds(Narator.clip.length);
    //    SoundManager.Instance.MainSource.volume = 0.5f;
    //    yield return new WaitForSeconds(1.5f);
    //    SoundManager.Instance.MainSource.volume = 1;
    //    StateMachine.Instance.IntroductionPlayed = true;
    //    StateMachine.Instance.currentState = GameStates.Playing;
    //}

}
