using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletHandler : MonoBehaviour
{
    public GameObject Projectile;
    public float bulletSpeed;

    Vector2 bulletPos;
    public float fireRate = 0.5F;
    float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire && Input.GetKeyDown("q"))
        {
            nextFire = Time.time + fireRate;
            fire();
        }
    }

    void fire()
    {
        float angle = this.transform.rotation.z;
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*Mathf.Cos(angle), bulletSpeed*Mathf.Sin(angle));
        //print(angle*(180f/Mathf.PI));
    }
}
