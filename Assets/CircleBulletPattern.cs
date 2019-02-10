using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletPattern : MonoBehaviour
{
    public GameObject Projectile;
    public float bulletSpeed;
    public float fireRate;
    public Sprite sprite;

    private Vector2 bulletPos;
    private bool fireCircleTime = true;

    float nextFire = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireCircle(int numBullets, bool hasSafespot = false)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            int a = -1;
            if (hasSafespot)
            {
                a = (int)(Random.value * numBullets)-1;
            }
            for (int i = 0; i < numBullets; ++i)
            {
                if (i == a || i == a+1)
                {
                    continue;
                }
                bulletPos = transform.position;
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * 0.5f * Mathf.Cos(i * 2 * Mathf.PI / numBullets), bulletSpeed * 0.5f * Mathf.Sin(i * 2 * Mathf.PI / numBullets));
                bullet.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }
}
