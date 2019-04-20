using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow.Legacy;

public class lightUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator flareUp()
    {
        Camera.main.GetComponents<MKGlow>()[2].bloomIntensity = 10f;
        yield return new WaitForSeconds(.2f);
    }
}
