using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool fireLaserTime = true;
    private int laserStage = 0;

    private float velocity;
    private float radius;
    private Vector2 _center;
    private float _angle;

    public GameObject Projectile;
    public GameObject Player;
    public float bulletSpeed;

    private Vector2 bulletPos;

    List<GameObject> laserBullets = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void laserSweep()
    {
        if ((int)Time.time % 10 == 0 && fireLaserTime == true && Time.time > 1f)
        {
            if (laserStage < 8)
            {
                bulletPos = transform.position;
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * laserStage * 0.15f, 0);
                GameObject bullet2 = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed * laserStage * 0.15f, 0);
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
            for (int i = 0; i < laserBullets.Count; ++i)
            {
                laserBullets[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        if (Time.time > 12 && Time.time < 14.1)
        {
            velocity = -1f * (Time.time - 12f) * .1f;
            _center = this.transform.position;
            for (int i = 0; i < laserBullets.Count; ++i)
            {
                radius = Mathf.Sqrt(Mathf.Pow((_center.x - laserBullets[i].transform.position.x), 2) + Mathf.Pow((_center.y - laserBullets[i].transform.position.y), 2));
                _angle += velocity * Time.deltaTime;
                var offset = new Vector2(Mathf.Sin(_angle - 1.57f), Mathf.Cos(_angle - 1.57f)) * radius;
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
