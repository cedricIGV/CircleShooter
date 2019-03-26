using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffBulletPattern : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Player;
    public float bulletSpeed;
    public float rotationalSpeed;
    public float bulletSparsity = 0.5F;
    public float fireRate = 10F;
    public Sprite sprite;
    float nextFire = 0.0F;
    float angle;
    private Vector2 initialDirection;
    Vector2 shotDirection;
    int direction;
    bool isFiring;
    float timeStamp;
    private Vector2 bulletPos;

    List<GameObject> bulletList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        isFiring = false;
        Vector2 shotDirection = new Vector2(0, 0);
        timeStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fireRiff()
    {
        if ((int)Time.time % fireRate == 0 && isFiring == false)
        {
            isFiring = true;
            initialDirection = -bulletSpeed * (Player.transform.position - this.transform.position).normalized;
            shotDirection = initialDirection;
        }
        if (Time.time > nextFire && isFiring && (direction == 1 || Time.time - timeStamp >=1))
        {
            nextFire = Time.time + bulletSparsity;
            bulletPos = transform.position;
            shotDirection = RotateVector(shotDirection, Time.deltaTime * rotationalSpeed*100*direction);
            GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shotDirection;
            bullet.GetComponent<SpriteRenderer>().sprite = sprite;
            bulletList.Add(bullet);
            if (Vector2.Angle(initialDirection, shotDirection) >= 170 && direction == 1)
            {
                initialDirection = -bulletSpeed * (Player.transform.position - this.transform.position).normalized;
                direction = direction * -1;
                shotDirection = initialDirection;
                Vector2 velocityNew;
                for (int i=1; i<=4; i++)
                {
                    velocityNew = (Player.transform.position - bulletList[bulletList.Count-3].transform.position).normalized;
                    bulletList[bulletList.Count - i].GetComponent<Rigidbody2D>().velocity = RotateVector(velocityNew * bulletSpeed,(int)Random.Range(0,((i-2)*-15)));
                }
                bulletList.Clear();
                timeStamp = Time.time;
            }
            else if (Vector2.Angle(initialDirection, shotDirection) >= 170 && direction == -1)
            {
                direction = direction * -1;
                isFiring = false;
                Vector2 velocityNew;
                for (int i = 1; i <= 4; i++)
                {
                    velocityNew = (Player.transform.position - bulletList[bulletList.Count - 3].transform.position).normalized;
                    bulletList[bulletList.Count - i].GetComponent<Rigidbody2D>().velocity = RotateVector(velocityNew * bulletSpeed, (int)Random.Range(0, ((i-2)*15)));
                }
                bulletList.Clear();
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
