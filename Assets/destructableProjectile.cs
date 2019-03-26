using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructableProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalHealth;
    public GameObject bulletExplosion;
    public Color flashColor = new Color(255, 255, 255, 45);
    private Color temp;
    public bool rotate = false;

    private int currentHealth;
    private Vector2 distanceFromCenter;
    public Vector3 bulletPos;
    private Vector2 offsetVectorTemp;
    private Vector3 offsetVector;
    public Vector3 velocity;
    void Start()
    {
        totalHealth = 4;
        currentHealth = totalHealth;
        temp = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        if(rotate)
        {
            distanceFromCenter = transform.position - bulletPos;
            offsetVectorTemp = Vector2.Perpendicular(distanceFromCenter);
            offsetVector = new Vector3(offsetVectorTemp.x, offsetVectorTemp.y, 0) * Time.deltaTime * 0.7f;
        }
        velocity = velocity + offsetVector;
    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        currentHealth=currentHealth - 1;
        StartCoroutine(Flash());
        if (currentHealth <= 0)
        {
            if (bulletExplosion != null)
            {
                Instantiate(bulletExplosion, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Flash()
    {
        float start = Time.time;
        while (Time.time - start < 1)
        {
            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void setVelocity(Vector3 vel)
    {
        velocity = vel;
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
