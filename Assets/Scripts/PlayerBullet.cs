using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public AudioSource asce;
    public float speed = 4.0f;

    Animator anim;

    private Rigidbody2D rg;
    void Start()
    {
        asce.Play();
        anim = GetComponent<Animator>();
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
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        anim.SetBool("Hit", true);
        rg.velocity = new Vector2(0, 0);
        Destroy(rg);
        Destroy(GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
