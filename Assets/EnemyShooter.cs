using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    //public Rigidbody2D projectile;
    //public float projectileSpeed = 8f;
    // Start is called before the first frame update
    public GameObject Projectile;
    public float bulletSpeed;
    public float rotationalSpeed;
    Vector2 bulletPos;
    public float fireRate = 0.5F;
    float nextFire = 0.0f;

    private bool fireCircleTime = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fireSpiral();
        }
        fireCircle();
    }

    void fireSpiral()
    {
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*Mathf.Cos(rotationalSpeed*Time.time*1f), bulletSpeed*Mathf.Sin(rotationalSpeed*Time.time*1f));
    }
    void fireCircle()
    {
        if ((int)Time.time % 4 == 0 && fireCircleTime == true)
        {
            for(int i =0; i<20; ++i)
            {
                bulletPos = transform.position;
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * 0.5f * Mathf.Cos(i * .3142f), bulletSpeed * 0.5f * Mathf.Sin(i * .3142f));
            }
            fireCircleTime = false;
        }
        if ((int)Time.time % 4 != 0 && fireCircleTime == false)
        {
            fireCircleTime = true;
        }
    }
}
