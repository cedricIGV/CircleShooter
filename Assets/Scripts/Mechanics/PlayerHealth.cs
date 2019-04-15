using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public float TotalHealth = 100;

    private float _currentHealth;
    bool dead;
    AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = TotalHealth;
        HealthBar.minValue = 0;
        _currentHealth = TotalHealth;
        HealthBar.value = _currentHealth;
        deathSound = GetComponent<AudioSource>();
        dead = false;
    }

    void Update()
    {
        if (_currentHealth <= 0 && dead == false)
        {
            die();
        }
    }

    void die()
    {
        AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
        dead = true;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;
        GetComponent<CamShakeSimple>().shakeDuration = .3f;
    }

    public float getCurrentHealth()
    {
        return _currentHealth;
    }
}
