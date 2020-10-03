using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float limRight, limLeft, limHeight, limLow;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private PlayerController player;

    [SerializeField]
    private int numberOfEnemiesMax = 16;

    private int currentNbOfEnemies;

    [SerializeField]
    private int numberOfEnemiesPerLine = 4;

    //used to make sur we only make the enemies go down once each time they arrive at the end of the line
    private bool goingRight = true;

    //might have to be replaced with the state machine pause button
    private bool paused = false;

    #region unitStats
    public float EnemySpeed = 3.0f;
    public float EnemySlowSpeed = 0.75f;
    public float DelayBetweenShoots = 2.0f;
    public float PlayerSpeed = 3.0f;

    [HideInInspector]
    public int NumberOfBonusUsed = 0;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;
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

    #region pauseButtonActions
    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        DefeatPanel.SetActive(true);
    }

    public void Close()
    {
        Application.Quit();
    }

    public void restart()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;
        Destroy(enemiesParent.gameObject);
        GameObject newParent = new GameObject();
        enemiesParent = newParent.transform;
        int numberOfLines = numberOfEnemiesMax / numberOfEnemiesPerLine;
        for (int i = 0; i < numberOfLines; i++)
        {
            for (int j = 0; j < numberOfEnemiesPerLine; j++)
            {
                GameObject go = Instantiate(EnemyPrefab, new Vector3(limLeft + 2* j, limHeight - 2 * i, 0), new Quaternion());
                go.transform.SetParent(enemiesParent);
            }
        }
        PausePanel.SetActive(false);
        VictoryPanel.SetActive(false);
        currentNbOfEnemies = numberOfEnemiesMax;
        DefeatPanel.SetActive(false);
        resume();
    }
    #endregion

    #region enemyRelatedFunctions
    public void EnemyKilled()
    {
        currentNbOfEnemies--;
        if (currentNbOfEnemies == 0)
        {
            VictoryPanel.SetActive(true);
        }
    }

    public void goDownRight(float speed)
    {
        if (!goingRight)
        {
            goingRight = true;

            for (int i = 0; i < enemiesParent.childCount; i++)
            {
                Transform t = enemiesParent.GetChild(i);
                t.position = new Vector3(t.position.x + 1, t.position.y - 2, 0);
                t.rotation = new Quaternion(t.rotation.x,0, t.rotation.z, t.rotation.w);
                enemiesParent.GetChild(i).transform.rotation = t.rotation;
                enemiesParent.GetChild(i).transform.position = t.position;
                enemiesParent.GetChild(i).GetComponent<Enemy>().rg.velocity = new Vector2(speed, 0);
            }
        }

    }

    public void goDownLeft(float speed)
    {
        if (goingRight)
        {
            goingRight = false;
            for (int i = 0; i < enemiesParent.childCount; i++)
            {
                Transform t = enemiesParent.GetChild(i);
                t.position = new Vector3(t.position.x - 1, t.position.y - 2, 0);
                t.rotation = new Quaternion(t.rotation.x, 180, t.rotation.z, t.rotation.w);
                enemiesParent.GetChild(i).transform.rotation = t.rotation;
                enemiesParent.GetChild(i).transform.position = t.position;
                enemiesParent.GetChild(i).GetComponent<Enemy>().rg.velocity = new Vector2(-speed, 0);
            }
        }
    }
    #endregion

    public void takeBonus()
    {

    }
}