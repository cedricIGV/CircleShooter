﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroid;
    public float bulletSpeed;
    public float fireRate = 10;
    public Sprite bulletSprite;

    float nextFire = 0;
    int numBullets = 10;
    Vector2 bulletPos;

    public bool fade;

    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 5.0f;
    public float startTime;
    Color oldColor;

    private List<GameObject> bullets = new List<GameObject>();
    void Start()
    {
        startTime = Time.time;
        fade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fade == true)
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if(bullets[i] != null)
                {
                    float t = (Time.time - startTime) / duration;
                    //print(Mathf.SmoothStep(minimum, maximum, t));
                    bullets[i].GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (Time.time - startTime)/duration);
                }
            }
        }
        else
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i] != null)
                {
                    float t = (Time.time - startTime) / duration;
                    //print(Mathf.SmoothStep(minimum, maximum, t));
                    bullets[i].GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
                }
            }
        }
    }

    public void launchAsteroid(bool rotate, Vector3 bulletPos, float angle)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            for (int i =0; i<numBullets; ++i)
            {
                angle = angle + 360 / numBullets;
                bulletPos = transform.position;
                GameObject bullet = Instantiate(asteroid, bulletPos, Quaternion.identity);
                Vector3 velocity = new Vector3(bulletSpeed * Mathf.Cos(Mathf.Deg2Rad*angle), bulletSpeed * Mathf.Sin(Mathf.Deg2Rad*angle), 0);
                bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
                bullet.GetComponent<destructableProjectile>().setVelocity(velocity);
                bullet.GetComponent<destructableProjectile>().rotate = rotate;
                bullet.GetComponent<destructableProjectile>().bulletPos = bulletPos;
                oldColor = bullet.GetComponent<SpriteRenderer>().color;
                bullets.Add(bullet);
            }
        }
    }
}