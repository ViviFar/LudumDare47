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
    public float PlayerSpeed = 7.0f;

    public float BonusCd = 3;
    [SerializeField]
    private float BaseBonusDuration = 5;
    [HideInInspector]
    public float CurrentBonusDuration;

    public int MaxBonusUsage = 5;
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
        currentLevel = 0;
        CurrentBonusDuration = BaseBonusDuration;
        curDelayShots = DelayBetweenShoots;
        previousState = currentState;
        NumberOfBonusUsed = 0;
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

    private void onLevelWonStateEnter()
    {
        currentLevel++;
        SceneManager.LoadScene("InBetweenLevels");
    }

    private void onQuittingStateEnter()
    {
        Application.Quit();
    }
    

    private void onGameOverStateEnter()
    {
        SceneManager.LoadScene("Menu");
    }

    private void onPlayingStateEnter()
    {
        SceneManager.LoadScene("Game");
    }

    private void onMenuStateEnter()
    {
        SceneManager.LoadScene("Menu");
        restartGame();
    }
}
