using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : GenericSingleton<StateMachine>
{

    public GameStates currentState = GameStates.Menu;
    private GameStates previousState;

    #region unitStats
    public float EnemySpeed = 5.0f;
    public float EnemySlowSpeed = 0.75f;
    [SerializeField]
    private float DelayBetweenShoots = 0.5f;
    public float BaseDelayBetweenShots { get { return DelayBetweenShoots; } }
    public float PlayerSpeed = 7.0f;

    [HideInInspector]
    public bool IntroductionPlayed = false;

    public float BonusCd = 3;
    [SerializeField]
    private float BaseBonusDuration = 10;
    [HideInInspector]
    public float CurrentBonusDuration;

    public int MaxBonusUsage = 8;
    [HideInInspector]
    public int NumberOfBonusUsed = 0;

    [HideInInspector]
    public float curDelayShots;
    #endregion

    int currentLevel = 0;
    public int CurrentLevel { get { return currentLevel; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        restartGame();
    }


    public  void restartGame()
    {
        IntroductionPlayed = false;
        currentLevel = 0;
        CurrentBonusDuration = BaseBonusDuration;
        curDelayShots = DelayBetweenShoots;
        previousState = currentState;
        NumberOfBonusUsed = 0;
        SoundManager.Instance.ChangeMainTheme(0);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != previousState)
        {
            switch (currentState)
            {
                case GameStates.Menu:
                    onMenuStateEnter();
                    break;
                case GameStates.Introduction:
                    onIntroductionStateEnter();
                    break;
                case GameStates.Playing:
                    onPlayingStateEnter();
                    break;
                case GameStates.GameOver:
                    onGameOverStateEnter();
                    break;
                case GameStates.Quitting:
                    onQuittingStateEnter();
                    break;
                case GameStates.LevelWon:
                    onLevelWonStateEnter();
                    break;
                default:
                    break;
            }
            previousState = currentState;
        }


    }

    private void onIntroductionStateEnter()
    {
        SceneManager.LoadScene("Introduction");
        SoundManager.Instance.PlayNarator(SoundManager.Instance.IntroFull, GameStates.Playing);
    }

    private void onLevelWonStateEnter()
    {
        GameController.Instance.EndBonus();
        currentLevel++;
        if (currentLevel % 6 == 0)
        {
            SceneManager.LoadScene("Introduction");
            SoundManager.Instance.PlayNarator(SoundManager.Instance.IntroReduced, GameStates.Playing);
        }
        else
        {
            SceneManager.LoadScene("InBetweenLevels");
            AudioClip[] clips = new AudioClip[2];
            clips[0] = SoundManager.Instance.nvx[(CurrentLevel % 6) - 1];
            clips[1] = SoundManager.Instance.nectars[NumberOfBonusUsed / 2];
            SoundManager.Instance.PlayNarator(clips, GameStates.Playing);
        }
    }

    private void onQuittingStateEnter()
    {
        Application.Quit();
    }
    

    private void onGameOverStateEnter()
    {
        SceneManager.LoadScene("GameOver");
        SoundManager.Instance.PlayNarator(SoundManager.Instance.conclusion, GameStates.Menu);
    }

    private void onPlayingStateEnter()
    {
        SceneManager.LoadScene("Game");
        SoundManager.Instance.MainSource.volume = 0.4f;
    }

    private void onMenuStateEnter()
    {
        SceneManager.LoadScene("Menu");
        restartGame();
    }
}
