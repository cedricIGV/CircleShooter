using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public GameObject deathParticlesPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void blowUp()
    {
        if (deathParticlesPrefab)
        {
            GameObject deathParticles = (GameObject)Instantiate(deathParticlesPrefab, transform.position, deathParticlesPrefab.transform.rotation);
            Destroy(deathParticles, deathParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
            gameObject.transform.SetParent(null);
            Destroy(gameObject);
        }

    }
}
