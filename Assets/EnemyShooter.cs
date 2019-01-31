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
    private bool fireLaserTime = true;
    private int laserStage = 0;

    private float velocity;
    private float radius;
    private Vector2 _center;
    private float _angle;

    public float offsetAngle;

    List<GameObject> laserBullets = new List<GameObject>();

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
        laserSweep();
    }

    void fireSpiral()
    {
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*Mathf.Cos(rotationalSpeed*Time.time*1f), bulletSpeed*Mathf.Sin(rotationalSpeed*Time.time*1f));
    }
    void fireCircle()
    {
        if ((int)Time.time % 4 == 0 && fireCircleTime == true && Time.time > 1f)
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
    void laserSweep()
    {
        if ((int)Time.time % 10 == 0 && fireLaserTime == true && Time.time > 1f)
        {
            if (laserStage < 8)
            {
                bulletPos = transform.position;
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*laserStage*0.15f, 0);
                GameObject bullet2 = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed*laserStage*0.15f, 0);
                laserBullets.Add(bullet);
                laserBullets.Add(bullet2);
            }
            laserStage++;
            if (laserStage >= 8)
            {
                fireLaserTime = false;
                laserStage = 0;
            }
        }
        if ((int)Time.time % 11 == 0)
        {
            for (int i =0; i<laserBullets.Count; ++i)
            {
                laserBullets[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        if (Time.time > 12 && Time.time < 14.1)
        {
            velocity = -1f * (Time.time - 12f) * .1f;
            _center = this.transform.position;
            for (int i = 0; i<laserBullets.Count; ++i)
            {
                radius = Mathf.Sqrt(Mathf.Pow((_center.x - laserBullets[i].transform.position.x),2) + Mathf.Pow((_center.y - laserBullets[i].transform.position.y),2));
                _angle += velocity * Time.deltaTime;
                var offset = new Vector2(Mathf.Sin(_angle -1.57f), Mathf.Cos(_angle - 1.57f)) * radius;
                laserBullets[i].transform.position = _center + offset;
            }
        }
        if (Time.time > 14.1)
        {
            for (int i = 0; i < laserBullets.Count; ++i)
            {
                GameObject a = laserBullets[i];
                laserBullets.RemoveAt(i);
                Destroy(a);
            }
        }
        if ((int)Time.time % 10 != 0 && fireLaserTime == false)
        {
            fireLaserTime = true;
        }
    }
}
