using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Color flashColor = new Color(255, 255, 255, 45);
    private Color temp;


    // Start is called before the first frame update
    void Start()
    {
        temp = GetComponent<SpriteRenderer>().color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            StartCoroutine(Flash());
            GetComponent<PlayerHealth>().TakeDamage(10);
        }
        
    }

    IEnumerator Flash()
    {
        GetComponent<SpriteRenderer>().color = flashColor;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = temp;
        yield return new WaitForSeconds(0.1f);
    }



}
