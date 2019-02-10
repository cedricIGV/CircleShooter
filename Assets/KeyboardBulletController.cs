using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardBulletController : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            GetComponent<FireAtPlayerPattern>().fireAtPlayer();
        }

        if (Input.GetKey("s")) {
            GetComponent<SpiralBulletPattern>().fireSpiral();
        }

        if (Input.GetKey("d"))
        {
            GetComponent<CircleBulletPattern>().fireCircle(15, true);
        }

        if(Input.GetKey("f"))
        {
            GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position);
        }


        //laserSweep();
    }






}
