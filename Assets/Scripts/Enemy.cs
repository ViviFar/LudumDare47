using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2;

    private Rigidbody2D rg;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 7 )
        {
            transform.position = new Vector3(transform.position.x-1, transform.position.y - 1, 0);
            speed *= -1;
            rg.velocity = new Vector2(speed, 0);
        }
        else if (transform.position.x < -7)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, 0);
            speed *= -1;
            rg.velocity = new Vector2(speed, 0);
        }
        if(transform.position.y<= -4.5)
        {
            GameController.Instance.Defeat();
        }
    }
    
}
