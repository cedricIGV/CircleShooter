﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    //public Rigidbody2D projectile;
    //public float projectileSpeed = 8f;
    // Start is called before the first frame update
    private int phase;
    public ScreenShake cameraShake;
    public GameObject explosion;
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<FireAtPlayerPattern>().fireAtPlayer();
        if (GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth*.90)
        {
            GetComponent<SpiralBulletPattern>().fireSpiral();
        }
        if (GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth * .80)
        {
            GetComponent<CircleBulletPattern>().fireCircle(15, true);

        }
        if (GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth * .79)
        {
            GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, 0);
        }
        if ((GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth * .9) && (phase==0))
        {
            cameraShake.TriggerShake();
            StartCoroutine(Explode());
            phase = 1;
        }
        //laserSweep();
    }

    IEnumerator Explode()
    {
        float start = Time.time;
        while (Time.time - start < 2)
        {
            Vector2 explosionPosition = Random.insideUnitCircle;
            Instantiate(explosion, explosionPosition, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }



}
