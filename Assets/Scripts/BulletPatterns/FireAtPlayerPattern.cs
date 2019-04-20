using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayerPattern : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Projectile;
    public GameObject Player;
    public float bulletSpeed;
    public float fireRate = 0.5F;
    public float numShots = 1;
    public float maxAngle = 0;
    float nextFire = 0.0F;

    public Vector2 spriteSize;

    private Vector2 bulletPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fireAtPlayer()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            bulletPos = transform.position;
            for (int i = (int)-numShots/2; i < numShots/2; ++i)
            {
                float a = (i*maxAngle)/numShots;
                Vector2 shotDirection = (Player.transform.position - this.transform.position).normalized;
                shotDirection = RotateVector(shotDirection, a);
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * shotDirection;
                bullet.GetComponent<SpriteRenderer>().size = spriteSize;
                //bullet.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
