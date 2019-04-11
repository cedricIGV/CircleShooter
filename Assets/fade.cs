using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public bool startFade;
    public float startTime;
    public float duration;
    Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        startFade = false;
        oldColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade == true)
        {
            float t = (Time.time - startTime) / duration;
            //print(Mathf.SmoothStep(minimum, maximum, t));
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (Time.time - startTime) / duration);
        }
        if (startFade == false)
        {
            GetComponent<SpriteRenderer>().color = oldColor;
        }

    }
}
