using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform tf;
    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 2.0f;
    // The initial position of the GameObject
    Vector3 initialPosition;
    Vector3 gridInit;
    GameObject grid;

    void Awake()
    {
        if (tf == null)
        {
            tf = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        grid = GameObject.FindGameObjectWithTag("Background");
        initialPosition = tf.localPosition;
        gridInit = grid.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void TriggerShake(float duration)
    {
        shakeDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            grid.transform.position = gridInit + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
            grid.transform.position = gridInit;
        }
    }
}
