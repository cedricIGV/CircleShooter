using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow.Legacy;
public class fade : MonoBehaviour
{
    //public bool startFade;
    //public float startTime;
    //public float duration;
    //Color oldColor;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    startFade = false;
    //    oldColor = GetComponent<SpriteRenderer>().color;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (startFade == true)
    //    {
    //        float t = (Time.time - startTime) / duration;
    //        GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (Time.time - startTime) / duration);
    //    }
    //    if (startFade == false)
    //    {
    //        GetComponent<SpriteRenderer>().color = oldColor;
    //        StartCoroutine(GlowTechniques.PulseMkGlow(Camera.main.GetComponents<MKGlow>()[2], Camera.main.GetComponents<MKGlow>()[2].bloomIntensity, 5f, 5f));
    //    }

    //}

    public static IEnumerator Fade(SpriteRenderer sprite, float startAlpha, float endAlpha, float duration)
    {
        // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;
        Color oldColor = sprite.color;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        sprite.color = new Color(oldColor.r, oldColor.g, oldColor.b, startAlpha);
        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are


            // calculate the new alpha
            if (startAlpha > endAlpha)
            {
                sprite.color = new Color(oldColor.r, oldColor.g, oldColor.b, startAlpha - (startAlpha-endAlpha)*percentage);
            }
            else
            {
                sprite.color = new Color(oldColor.r, oldColor.g, oldColor.b, startAlpha + (endAlpha - startAlpha) * percentage);
            }




            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        //Debug.Log(endTime / 2);
        sprite.color = new Color(oldColor.r, oldColor.g, oldColor.b, endAlpha); // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
    }

    public  IEnumerator Peak(SpriteRenderer sprite, float startAlpha, float peakAlpha, float endAlpha,  float duration)
    {
        yield return StartCoroutine(Fade(sprite, startAlpha, peakAlpha, duration * .75f));
        StartCoroutine(Fade(sprite, peakAlpha, endAlpha, duration*.25f));
    }

}
