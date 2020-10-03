﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;

    [HideInInspector]
    public Rigidbody2D rg;

    // Start is called before the first frame update
    void Start()
    {
        speed = StateMachine.Instance.EnemySpeed;
        rg = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitBeforeLaunch());
    }

    private IEnumerator WaitBeforeLaunch()
    {
        yield return new WaitForSeconds(3);
        rg.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > GameController.Instance.limRight+1 )
        {
            GameController.Instance.goDownLeft(speed);
        }
        else if (transform.position.x < GameController.Instance.limLeft-1)
        {
            GameController.Instance.goDownRight(speed);
        }
        if(transform.position.y<= GameController.Instance.limLow)
        {
            GameController.Instance.Defeat();
        }
    }

    public void UpdateSpeed(float newSpeed)
    {

    }

}