using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBulletPattern : MonoBehaviour
{
    // Start is called before the first frame update
    //private EnemyShooter boss = getComponent<EnemyShooter>();
    public GameObject Projectile;
    public float bulletSpeed;
    public float rotationalSpeed;
    public float fireRate = 0.5F;
    public Sprite sprite;
    float nextFire = 0.0F;

    private Vector2 bulletPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void fireSpiral()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            bulletPos = transform.position;
            GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(rotationalSpeed * Time.time * 1f), bulletSpeed * Mathf.Sin(rotationalSpeed * Time.time * 1f));
            bullet.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

}
