using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{
    #region singleton
    private static StateMachine instance;
    public static StateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StateMachine>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<StateMachine>();
                    go.name = "StateMachine";
                }
            }
            return instance;
        }
    }
    #endregion

    private GameStates previousState = GameStates.Menu;
    public GameStates currentState = GameStates.Menu;

    [HideInInspector]
    public bool Paused = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (currentState != previousState)
        {
            switch (currentState)
            {
                case GameStates.Menu:
                    onEnterMenuState();
                    break;
                case GameStates.Playing:
                    onEnterPlayingState();
                    break;
            }
            previousState = currentState;
        }
    }

    private void onEnterPlayingState()
    {
        SceneManager.LoadScene("Game");
    }

    private void onEnterMenuState()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetNextState(GameStates newState)
    {
        currentState = newState;
    }
}
