using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGapPattern : MonoBehaviour
{
    public GameObject Projectile;
    public float bulletSpeed;

    private Vector2 bulletPos;
    private List<List<GameObject> > bullets = new List<List<GameObject> >();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnLasers()
    {
        bulletPos = transform.position;
        for (int j=0; j<5; ++j)
        {
            int blankSpot = (int)(Random.value * 5) + 10;
            List<GameObject> row = new List<GameObject>();
            for (int i = 0; i < 20; ++i)
            {
                if (i == blankSpot || i == blankSpot + 1 || i == blankSpot - 1)
                {
                    continue;
                }
                GameObject bullet = Instantiate(Projectile, bulletPos, Quaternion.identity);
                bullet.GetComponent<BulletController>().SetType("laser");
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2((bulletSpeed * ((float)i / 10))*Mathf.Cos((float)(6.28/5.0) * j), (bulletSpeed * ((float)i / 10)) * Mathf.Sin((float)(6.28 / 5.0) * j));
                bullet.GetComponent<BulletController>().dissapearOffscreen = false;
                row.Add(bullet);
                //string type = bullet.GetComponent<BulletController>().type;
            }
            bullets.Add(row);
        }
    }

    public void RotateLasers()
    {
        for (int i=0; i<bullets.Count; ++i)
        {
            int direction = 1;
            if (i % 2 == 1)
            {
                direction = direction * -1;
            }
            if ((int)Time.time % 2 == 1)
            {
                direction = direction * -1;
            }
            for (int j=0; j<bullets[i].Count; ++j)
            {
                if (bullets[i][j] != null)
                {
                    Vector2 distanceFromCenter;
                    Vector2 offsetVector = new Vector2(0, 0);
                    Vector2 pos2d = new Vector2(bullets[i][j].GetComponent<Transform>().position.x, bullets[i][j].GetComponent<Transform>().position.y);
                    distanceFromCenter = pos2d - bulletPos;
                    Vector2 offsetVector2 = distanceFromCenter.normalized * direction;
                    offsetVector = Vector2.Perpendicular(distanceFromCenter) * Time.deltaTime * bulletSpeed * 10;
                    bullets[i][j].GetComponent<Rigidbody2D>().velocity = offsetVector + offsetVector2;
                }
            }
        }
    }
}
