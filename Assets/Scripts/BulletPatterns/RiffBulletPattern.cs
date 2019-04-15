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
    public Sprite sprite;
    private Vector2 initialDirection;
    Vector2 shotDirection;
    private Vector2 bulletPos;

    List<GameObject> bulletList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator fireRiff()
    {
        initialDirection = -bulletSpeed * (Player.transform.position - this.transform.position).normalized;
        shotDirection = initialDirection;
        for (int i =0; i<14; ++i)
        {
            print(i);
            bulletPos = transform.position;
            shotDirection = RotateVector(shotDirection, i * rotationalSpeed * .1f);
            float angle = Vector2.Angle(shotDirection, initialDirection);
            Vector2 shotDirection2 = RotateVector(shotDirection, -angle*2); //flip shotDirection over line btwn player and enemy
            GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
            GameObject bullet2 = Instantiate(Projectile, bulletPos, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shotDirection + (shotDirection * i*.1f);
            bullet2.GetComponent<Rigidbody2D>().velocity = shotDirection2 + (shotDirection2 * i * .1f);
            yield return new WaitForSeconds(0.1f);
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
