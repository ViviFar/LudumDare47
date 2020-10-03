using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public float speed = 3.0f;
    public float MaxLimit, MinLimit;

    public float DelayBetweenShoots = 2.0f;

    private bool canShoot = true;
    private float timeSinceShoot = 0;

    // Update is called once per frame
    void Update()
    {
        //arbitrary decision: if the player enters both right and left arrows, right one will prevail
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            pos.x = Mathf.Min(MaxLimit, pos.x);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            pos.x = Mathf.Max(MinLimit, pos.x);
            transform.position = pos;
        }

        timeSinceShoot += Time.deltaTime;

        //reset the ability to shoot if cooldown is over
        if (timeSinceShoot > DelayBetweenShoots)
        {
            canShoot = true;
        }

        //shoot and lock the ability for the cooldown time
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            GameObject go = Instantiate(bulletPrefab, transform);
            go.transform.SetParent(bulletParent);
            canShoot = false;
            timeSinceShoot = 0;
        }
    }
}
