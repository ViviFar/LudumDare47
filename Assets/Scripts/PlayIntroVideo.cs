using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayIntroVideo : MonoBehaviour
{
    public VideoClip introFull, introShort;

    public VideoPlayer vp;

    private void Start()
    {
        vp.Stop();
        if (StateMachine.Instance.currentState == GameStates.Introduction)
        {
            vp.clip = introFull;
        }
        else
        {
            vp.clip = introShort;
        }
        vp.Play();
    }

}
