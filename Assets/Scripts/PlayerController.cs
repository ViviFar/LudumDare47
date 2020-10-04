using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public float MaxLimit, MinLimit;

    private float speed;
    private float delayBetweenShoots;

    private bool canShoot = true;
    private float timeSinceShoot = 0;
    float startTimer;
    private void Start()
    {
        speed = StateMachine.Instance.PlayerSpeed;
        delayBetweenShoots = StateMachine.Instance.curDelayShots;
        startTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer <= 3)
        {
            startTimer += Time.deltaTime;
            return;
        }
        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        pos.x = Mathf.Min(MaxLimit, pos.x);
        pos.x = Mathf.Max(MinLimit, pos.x);
        transform.position = pos;


        timeSinceShoot += Time.deltaTime;

        //reset the ability to shoot if cooldown is over
        if (timeSinceShoot > delayBetweenShoots)
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
