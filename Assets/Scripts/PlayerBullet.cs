using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 4.0f;

    private Rigidbody2D rg;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(0, speed);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameController.Instance.EnemyKilled();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
