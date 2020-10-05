using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ControlPanel;
    public GameObject CreditsPanel;

    private bool isInMainMenu = true;


    // Start is called before the first frame update
    void Start()
    {
        GoToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInMainMenu)
            {
                Close();
            }
            else
            {
                GoToMenu();
            }
        }
    }

    public void Play()
    {
        StateMachine.Instance.currentState = GameStates.Introduction;
    }

    public void GoToMenu()
    {
        MainMenuPanel.SetActive(true);
        ControlPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        isInMainMenu = true;
    }

    public void GoToControls()
    {
        MainMenuPanel.SetActive(false);
        ControlPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        isInMainMenu = false;
    }
    public void GoToCredits()
    {
        MainMenuPanel.SetActive(false);
        ControlPanel.SetActive(false);
        CreditsPanel.SetActive(true);
        isInMainMenu = false;
    }

    public void Close()
    {
        StateMachine.Instance.currentState = GameStates.Quitting;
    }
}
