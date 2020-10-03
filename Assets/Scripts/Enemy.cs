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
        rg.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 7 )
        {
            GameController.Instance.goDownLeft(speed);
        }
        else if (transform.position.x < -7)
        {
            GameController.Instance.goDownRight(speed);
        }
        if(transform.position.y<= -4.5)
        {
            GameController.Instance.Defeat();
        }
    }

    public void UpdateSpeed(float newSpeed)
    {

    }

}
