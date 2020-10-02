using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameStates currentState = GameStates.Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
