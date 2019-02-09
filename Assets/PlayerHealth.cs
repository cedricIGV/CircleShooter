using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public float TotalHealth = 100;

    private float _currentHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = TotalHealth;
        HealthBar.minValue = 0;
        _currentHealth = TotalHealth;
        HealthBar.value = _currentHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;
    }

    public float getCurrentHealth()
    {
        return _currentHealth;
    }
}
