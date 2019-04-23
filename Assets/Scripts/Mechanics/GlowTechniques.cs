using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow.Legacy;

public class GlowTechniques : MonoBehaviour
{
    public static IEnumerator PulseMkGlow(MKGlow glow, float startAlpha, float endAlpha, float duration)
    {
        // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        glow.bloomScattering = startAlpha;
        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (Time.time <= (endTime / 2)) // if we are fading up 
            {
                glow.bloomScattering = startAlpha + (endAlpha - startAlpha) * percentage; // calculate the new alpha
                //Debug.Log(endTime / 2);
            }
            else // if we are fading down
            {
                glow.bloomScattering = endAlpha - (endAlpha - startAlpha) * percentage; // calculate the new alpha
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        //Debug.Log(endTime / 2);
        glow.bloomScattering = startAlpha; // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
    }
}
