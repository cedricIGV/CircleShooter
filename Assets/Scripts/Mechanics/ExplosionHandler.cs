using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float explodeTime;
    private float initialTime;
    void Start()
    {
        initialTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > initialTime + explodeTime)
        {
            Destroy(this.gameObject);
        }
        
    }
}
