using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public HealthBar healthBar;
    
    private float health;
    public void SetHealthBar(HealthBar _hb)
    {
        healthBar = _hb;
    }
    void Start()
    {
        health = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(20f);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health,maxHealth);
    }
}
