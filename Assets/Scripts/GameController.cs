using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PausePanel;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
            else
            {
                resume();
            }
        }
    }

    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void LoadMenu()
    {
        StateMachine.Instance.currentState = GameStates.Menu;
    }

    public void Close()
    {
        Application.Quit();
    }
}
