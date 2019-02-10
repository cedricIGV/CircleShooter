using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    //public Rigidbody2D projectile;
    //public float projectileSpeed = 8f;
    // Start is called before the first frame update
    private int phase;
    public ScreenShake cameraShake;
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<FireAtPlayerPattern>().fireAtPlayer();
        if (GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth*.90)
        {
            GetComponent<SpiralBulletPattern>().fireSpiral();
        }
        if (GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth * .80)
        {
            GetComponent<CircleBulletPattern>().fireCircle(15, true);
            GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position);
        }
        if ((GetComponent<PlayerHealth>().getCurrentHealth() < GetComponent<PlayerHealth>().TotalHealth * .7) && (phase==0))
        {
            cameraShake.TriggerShake();
        }
        //laserSweep();
    }

    




}
