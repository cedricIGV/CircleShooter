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
    // Start is called before the first frame update
    void Start()
    {
        _center = pivot.transform.position;
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
    }
}
