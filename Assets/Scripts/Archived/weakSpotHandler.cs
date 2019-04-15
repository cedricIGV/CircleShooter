using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakSpotHandler : MonoBehaviour
{
    public GameObject pivot;
    public float offsetAngle;

    private float velocity = 1;
    private float radius = 1;
    private Vector2 _center;
    private float _angle;

    public Color flashColor = new Color(255, 255, 255, 45);
    private Color temp;
    // Start is called before the first frame update
    void Start()
    {
        _center = pivot.transform.position;
        temp = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        _angle += velocity * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(_angle + offsetAngle), Mathf.Cos(_angle+offsetAngle)) * radius;
        transform.position = _center + offset;
    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        StartCoroutine(Flash());
        pivot.GetComponent<PlayerHealth>().TakeDamage(10);
    }

    IEnumerator Flash()
    {
        float start = Time.time;
        while(Time.time - start < 2)
        {
            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
