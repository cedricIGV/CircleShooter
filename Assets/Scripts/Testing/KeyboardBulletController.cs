using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyboardBulletController : MonoBehaviour
{
    float angle = 0;
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
            GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, angle+=20, 0);
        }


        if (Input.GetKey("w"))
        {
            StartCoroutine("AsteroidCircle");
        }
        if (Input.GetKey("e"))
        {
            StartCoroutine(FireAtPlayerBurst(5));
        }



        //laserSweep();
    }
    IEnumerator AsteroidCircle()
    {
        GetComponent<CircleBulletPattern>().fireCircle(15, true);
        yield return new WaitForSeconds(.3f);
        GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, angle += 20, 0);
        yield return new WaitForSeconds(.3f);
        GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, angle += 20, 0);

    }

    IEnumerator FireAtPlayerBurst(int x)
    {
        for (int a = 0; a < x; a++)
        {
            GetComponent<FireAtPlayerPattern>().fireAtPlayer();
            yield return new WaitForSeconds(.3f);
        }
    }
}
