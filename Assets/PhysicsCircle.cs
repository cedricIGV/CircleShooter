using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCircle : MonoBehaviour
{
    public float centerX;
    public float centerY;
    public float radius;
    public float angle;
    public float fireRate = 0.25f;
    float nextFire = 0.0f;

    public int vertexCount = 40;
    public float lineWidth = .3f;

    public Color flashColor = new Color(255, 255, 255, 45);

    public GameObject Projectile;
    public float bulletSpeed;

    private Vector2 bulletPos;


    private LineRenderer lineRenderer;
    private Color temp;

    private Rigidbody2D body;

    private bool invincible = false;
    private bool blasting = false;

    private int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        temp = GetComponent<SpriteRenderer>().color;

        body = GetComponent<Rigidbody2D>();

        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        angle = Mathf.Atan2(transform.position.y, transform.position.x);
        radius = transform.position.magnitude;
        SetupCircle();

        //if (Input.GetKeyDown("left"))
        //{
        //    body.velocity = Vector2.zero;
        //}
        //if (Input.GetKeyDown("right"))
        //{
        //    body.velocity = Vector2.zero;
        //}

        if (Time.time > nextFire && Input.GetKey("q"))
        {
            nextFire = Time.time + fireRate;
            fire();
            body.AddForce(50*transform.position.normalized);
            //transform.eulerAngles = new Vector3(0, 0, angle - 90);

        }

        else if (Input.GetKey("left"))
        {
            //body.velocity = 5 * new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle));
            body.AddForce(new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle)));
            //angle += 4;
            //transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            //body.velocity = Vector2.zero;
            //transform.position += (new Vector3(Mathf.Sin(angle), -Mathf.Cos(angle), 0))/5;

            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle - 90);

        }
        else if (Input.GetKey("right"))
        {
            //body.velocity = 5 * new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
            body.AddForce(new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle)));
            //angle -= 4;
            //transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            //body.velocity = Vector2.zero;


            //transform.position += (new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle),0))/5;
            //body.AddForce(-3 * transform.position.normalized);
            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle - 90);
        }


            if (radius > 1)
            {
                body.AddForce(-1 * transform.position.normalized);
            }
            else
            {
                body.velocity = Vector2.zero;
            }

        
        //body.velocity = 500 * new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle));

        //body.velocity = Vector2.zero;





        //transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));

        //if(radius > 0)
        //{
        //    radius -= .01f*frameCount;
        //    frameCount++;

        //}
        //if (Time.time > nextFire && Input.GetKey("q"))
        //{
        //    nextFire = Time.time + fireRate;
        //    fire();
        //    blasting = true;
        //    StartCoroutine(Blast());
        //    Invoke("resetBlast", fireRate);

        //}


        //if (Time.timeScale == 1)
        //{
        //    if (Time.time > nextFire && Input.GetKey("q"))
        //    {
        //        nextFire = Time.time + fireRate;
        //        fire();
        //    }
        //    if (Input.GetKey("left"))
        //    {
        //        angle += 30 / (radius * radius);
        //        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        //        transform.eulerAngles = new Vector3(0, 0, angle - 90);

        //    }
        //    else if (Input.GetKey("right"))
        //    {
        //        angle -= 30 / (radius * radius);
        //        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        //        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        //    }

        //    if (Input.GetKey("up") && radius < Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
        //            Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - 2 * lineWidth)
        //    {
        //        radius += .1f;
        //        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        //        SetupCircle();
        //        //circle.transform.localScale += new Vector3(.0365f,.0365f,0);

        //    }
        //    else if (Input.GetKey("down") && radius > 1)
        //    {
        //        radius -= .1f;
        //        transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        //        SetupCircle();
        //        //circle.transform.localScale -= new Vector3(.0365f, .0365f, 0);
        //    }
        //}
    }


   

    void resetInvulnerability()
    {
        invincible = false;
        GetComponent<CapsuleCollider2D>().enabled = true;

    }

    void resetBlast()
    {
        blasting = false;
        frameCount = 0;
    }

    void fire()
    {
        bulletPos = transform.position;
        GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * bulletSpeed * Mathf.Cos(Mathf.Deg2Rad * angle), -1 * bulletSpeed * Mathf.Sin(Mathf.Deg2Rad * angle));
        bullet.transform.eulerAngles = new Vector3(0, 0, angle - 90 - 180);
    }

    IEnumerator Flash()
    {

        while (invincible)
        {

            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.1f);

        }
    }

    IEnumerator Blast()
    {
        frameCount = 0;
        while (blasting)
        {

            radius = .02f * frameCount;
            yield return new WaitForSeconds(0.0f);
            frameCount++;
            print(radius);
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
