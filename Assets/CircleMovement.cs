using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float centerX;
    public float centerY;
    public float radius;
    public float angle;

    public int vertexCount = 40;
    public float lineWidth = .3f;

    public Color flashColor = new Color(255,255,255, 45);



    private LineRenderer lineRenderer;
    private Color temp;


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


        if (Input.GetKey("left"))
        {
            angle += 10/radius;
            transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            transform.eulerAngles = new Vector3(0,0,angle-90);

        }
        else if (Input.GetKey("right"))
        {
            angle -= 10/radius;
            transform.position = new Vector2(centerX + radius * Mathf.Cos(Mathf.Deg2Rad * angle), centerY + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }

        if (Input.GetKey("up") && radius < Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
                Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - 2*lineWidth)
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

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                invincible = true;
                StartCoroutine(Flash());
                GetComponent<CapsuleCollider2D>().enabled = false;
                Invoke("resetInvulnerability", 2);
            }
        }
    }

    void resetInvulnerability()
    {
        invincible = false;
        GetComponent<CapsuleCollider2D>().enabled = true;

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
