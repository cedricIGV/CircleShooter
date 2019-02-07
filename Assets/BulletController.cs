﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float velX = 5f;
    public GameObject bulletExplosion;
    public bool exploding = false;
    public GameObject Projectile;
    public int numBullets;
    public int bulletSpeed;

    private Vector3 start;
    

    float velY = 0f;
    Rigidbody2D rb;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        start = transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        if (exploding && (start - transform.position).magnitude > 3)
        {
            for (int i = 0; i < numBullets; ++i)
            {
                GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * 0.5f * Mathf.Cos(i * 2 * Mathf.PI / numBullets), bulletSpeed * 0.5f * Mathf.Sin(i * 2 * Mathf.PI / numBullets));
            }
            exploding = false;

        }
    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        if (bulletExplosion != null)
        {
            Instantiate(bulletExplosion, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
