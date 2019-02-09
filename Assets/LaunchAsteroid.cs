using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroid;
    public float bulletSpeed;
    public float fireRate = 10;
    float angle = 0;
    float nextFire = 0;
    int numBullets = 10;
    Vector2 bulletPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void launchAsteroid(bool rotate, Vector3 bulletPos)
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
                bullet.GetComponent<destructableProjectile>().setVelocity(velocity);
                bullet.GetComponent<destructableProjectile>().rotate = rotate;
                bullet.GetComponent<destructableProjectile>().bulletPos = bulletPos;
            }
        }
    }
}