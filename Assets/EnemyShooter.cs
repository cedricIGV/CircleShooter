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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            //var fireballInst = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector2(0, 0)));
            //fireballInst.velocity = new Vector2(projectileSpeed, 0);
            nextFire = Time.time + fireRate;
            fire();
        }
    }

    void fire()
    {
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*Mathf.Cos(rotationalSpeed*Time.time*1f), bulletSpeed*Mathf.Sin(rotationalSpeed*Time.time*1f));

    }
}
