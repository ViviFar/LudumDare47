using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutScene : MonoBehaviour
{
    private float cutsceneDuration = 5.5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(cutsceneDuration);
        StateMachine.Instance.currentState = GameStates.Playing;
    }
}
