using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<GameController>();
                    go.name = "StateMachine";
                }
            }
            return instance;
        }
    }
    #endregion


    public GameObject PausePanel;
    public GameObject VictoryPanel, DefeatPanel;
    public GameObject EnemyPrefab;
    public Transform enemiesParent;

    [SerializeField]
    private int numberOfEnemiesMax = 16;

    private int currentNbOfEnemies;

    [SerializeField]
    private int numberOfEnemiesPerLine = 4;

    //might have to be replaced with the state machine pause button
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        restart();
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

    public void restart()
    {
        Destroy(enemiesParent.gameObject);
        GameObject newParent = new GameObject();
        enemiesParent = newParent.transform;
        int numberOfLines = numberOfEnemiesMax / numberOfEnemiesPerLine;
        for (int i = 0; i < numberOfLines; i++)
        {
            for (int j = 0; j < numberOfEnemiesPerLine; j++)
            {
                GameObject go = Instantiate(EnemyPrefab, new Vector3(-6 + j, 4.5f - i, 0), new Quaternion());
                go.transform.SetParent(enemiesParent);
            }
        }
        PausePanel.SetActive(false);
        VictoryPanel.SetActive(false);
        currentNbOfEnemies = numberOfEnemiesMax;
        DefeatPanel.SetActive(false);
        resume();
    }

    public void EnemyKilled()
    {
        currentNbOfEnemies--;
        if (currentNbOfEnemies == 0)
        {
            VictoryPanel.SetActive(true);
        }
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        DefeatPanel.SetActive(true);
    }
}
