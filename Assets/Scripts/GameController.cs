using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : GenericSingleton<GameController>
{

    public GameObject PausePanel;
    public GameObject EnemyPrefab;
    public Transform enemiesParent;
    public Button BonusButton;
    public float limRight, limLeft, limHeight, limLow;

    public Image img;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private PlayerController player;

    #region enemyOrganization
    [SerializeField]
    private int numberOfEnemiesMax = 16;
    [SerializeField]
    private int numberOfEnemiesPerLine = 4;

    private int currentNbOfEnemies;
    #endregion

    //used to make sur we only make the enemies go down once each time they arrive at the end of the line
    private bool goingRight = true;
    public bool GoingRight { get { return goingRight; } }

    //might have to be replaced with the state machine pause button
    private bool paused = false;

    public float bonusDuration = 5;


    public Text CountdownText;
    private float startCountdown = 3;

    // Start is called before the first frame update
    void Start()
    {
        int moreEnemies = (StateMachine.Instance.CurrentLevel + 1) / 3;
        numberOfEnemiesMax += 4 * moreEnemies;
        numberOfEnemiesPerLine= 4 + (StateMachine.Instance.CurrentLevel +1) / 3;
        player = FindObjectOfType<PlayerController>();
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;
        Restart();
    }


    // Update is called once per frame
    void Update()
    {
        if (startCountdown > 0)
        {
            startCountdown -= Time.deltaTime;
            int cd = (int)startCountdown+1;
            CountdownText.text = cd.ToString();
        }
        else
        {
            CountdownText.gameObject.SetActive(false);
        }
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
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.W)){
            StateMachine.Instance.currentState = GameStates.LevelWon;
        }
        if(Input.GetKeyDown(KeyCode.L)){
            Defeat();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StateMachine.Instance.currentState = GameStates.GameOver;
        }
#endif
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Defeat()
    {
        img.gameObject.SetActive(true);
        img.GetComponent<Fade>().fading = true;
        //StateMachine.Instance.currentState = GameStates.LevelLost;
        StartCoroutine(LaunchRestart());
    }

    public void Close()
    {
        StateMachine.Instance.currentState = GameStates.Quitting;
    }

    private IEnumerator LaunchRestart()
    {
        yield return new WaitForSeconds(3);
        Restart();
    }

    public void Restart()
    {
        Destroy(enemiesParent.gameObject);
        GameObject newParent = new GameObject();
        enemiesParent = newParent.transform;
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;
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
        currentNbOfEnemies = numberOfEnemiesMax;
        player.Restart();
        img.GetComponent<Fade>().fading = false;
        goingRight = true;
        startCountdown = 3;
        CountdownText.gameObject.SetActive(true);
        //img.gameObject.SetActive(false);
        resume();
    }
#endregion

#region enemyRelatedFunctions
    public void EnemyKilled()
    {
        currentNbOfEnemies--;
        if (currentNbOfEnemies == 0)
        {
            StateMachine.Instance.currentState = GameStates.LevelWon;
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

    public void TakeBonus()
    {
        StateMachine.Instance.NumberOfBonusUsed++;
        if(StateMachine.Instance.NumberOfBonusUsed == StateMachine.Instance.MaxBonusUsage)
        {
            StateMachine.Instance.currentState = GameStates.GameOver;
            return;
        }
        for (int i = 0; i < enemiesParent.childCount; i++)
        {
            enemiesParent.GetChild(i).GetComponent<Enemy>().UpdateSpeed(StateMachine.Instance.EnemySlowSpeed);
        }
        GraphicsController.Instance.UpdateSprites(true);
        SoundManager.Instance.launchHigh();
        player.UpdateAs(StateMachine.Instance.BaseDelayBetweenShots);
    }

    public void EndBonus()
    {
        for (int i = 0; i < enemiesParent.childCount; i++)
        {
            enemiesParent.GetChild(i).GetComponent<Enemy>().UpdateSpeed(StateMachine.Instance.EnemySpeed);
        }
        StateMachine.Instance.curDelayShots += (0.1f * StateMachine.Instance.NumberOfBonusUsed);
        StateMachine.Instance.CurrentBonusDuration -= 1;
        player.UpdateAs(StateMachine.Instance.curDelayShots);
        GraphicsController.Instance.UpdateSprites(false);
        SoundManager.Instance.ChangeMainTheme(StateMachine.Instance.NumberOfBonusUsed / 2);
    }

}