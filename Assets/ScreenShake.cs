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
    private float shakeMagnitude = 0.7f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (tf == null)
        {
            tf = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = tf.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void TriggerShake()
    {
        shakeDuration = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
            print(shakeDuration);
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
}
