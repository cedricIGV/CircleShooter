using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float centerX;
    public float centerY;
    public float radius;
    public float angle;
    public float fireRate = 0.25f;
    float nextFire = 0.0f;

    public int vertexCount = 40;
    public float lineWidth = .3f;

    public Color flashColor = new Color(255,255,255, 45);

    public GameObject Projectile;
    public float bulletSpeed;

    private Vector2 bulletPos;


    private LineRenderer lineRenderer;
    private Color temp;


    private float angleInc = 30;


    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        temp = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {

            if (Input.GetKeyDown("q"))
            {
                angleInc = angleInc / 2;
            }
            else if (Input.GetKeyUp("q"))
            {
                angleInc = angleInc * 2; // 2;
            }

            if (Input.GetKey("q"))
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    fire();

                }
                /*if (radius < Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
                    Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - 2 * lineWidth)
                {
                    radius += .07f;
                    transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
                    SetupCircle();

                }*/
            }
            //else
            //{
            //    if (radius > 1)
            //    {
            //        radius -= .05f;
            //        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            //        SetupCircle();
            //    }

            //}

            if (Input.GetKey("left"))
            {
                angle += angleInc / (radius*radius);
                angle = angle % 360;
                transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
                transform.eulerAngles = new Vector3(0, 0, angle - 90);

            }
            else if (Input.GetKey("right"))
            {
                angle -= angleInc / (radius*radius);
                angle = angle % 360;
                transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
                transform.eulerAngles = new Vector3(0, 0, angle - 90);
            }

            if (Input.GetKey("up") && radius < Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
                    Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - 2 * lineWidth)
            {
                radius += .1f;
                transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
                SetupCircle();
                //circle.transform.localScale += new Vector3(.0365f,.0365f,0);

            }
            else if (Input.GetKey("down") && radius > 1)
            {
                radius -= .1f;
                transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
                SetupCircle();
                //circle.transform.localScale -= new Vector3(.0365f, .0365f, 0);
            }

        }
    }

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                invincible = true;
                StartCoroutine(Flash());
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<PlayerHealth>().TakeDamage(10);
                Invoke("resetInvulnerability", 2);
            }
        }
    }

    void resetInvulnerability()
    {
        invincible = false;
        GetComponent<CapsuleCollider2D>().enabled = true;

    }

    void fire()
    {
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * bulletSpeed * Mathf.Cos(Mathf.Deg2Rad* angle), -1 * bulletSpeed * Mathf.Sin(Mathf.Deg2Rad * angle));
        bullet.transform.eulerAngles = new Vector3(0, 0, angle - 90 - 180);
    }

    IEnumerator Flash()
    {

        while(invincible)
        {

            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Burst()
    {
        float sum = 0;
        while (sum < 0.5f)
        {
            yield return new WaitForSeconds(0.0f);
            sum += .1f;
            radius += .1f;
            transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            SetupCircle();
        }
    }


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupCircle();
    }

    private void SetupCircle()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.widthMultiplier = lineWidth;

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = transform.position;

        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }
    }
#endif
}
