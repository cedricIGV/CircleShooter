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
    public bool fade;
    public float duration = 5.0f;
    public float startTime;
    float nextFire = 0.0F;
    Color oldColor;

    public Vector2 spriteSize;

    private Vector2 bulletPos;
    List<GameObject> bullets = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fade == true)
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i] != null)
                {
                    float t = (Time.time - startTime) / duration;
                    //print(Mathf.SmoothStep(minimum, maximum, t));
                    bullets[i].GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (Time.time - startTime) / duration);
                    if ((oldColor.a - (Time.time - startTime) / duration) < .7)
                    {
                        foreach(Collider2D c in bullets[i].GetComponents<Collider2D> ())
                        {
                            c.enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i] != null)
                {
                    float t = (Time.time - startTime) / duration;
                    //print(Mathf.SmoothStep(minimum, maximum, t));
                    bullets[i].GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
                }
            }
        }
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
                oldColor = bullet.GetComponent<SpriteRenderer>().color;
                bullets.Add(bullet);
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
