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
    public float DelayBetweenShoots = 0.5f;
    public float PlayerSpeed = 7.0f;

    [HideInInspector]
    public int NumberOfBonusUsed = 0;

    private float curDelayShots;
    #endregion

    int currentLevel = 0;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        curDelayShots = DelayBetweenShoots;
        previousState = currentState;
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
        //resetting stats so that new game isnt impacted by older one
        currentLevel = 0;
        curDelayShots = DelayBetweenShoots;
        NumberOfBonusUsed = 0;
    }
}
