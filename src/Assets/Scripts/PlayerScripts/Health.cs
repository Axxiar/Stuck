using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    
    private HealthBar healthBar;
    public float health;
    public void SetHealthBar(HealthBar _hb)
    {
healthBar = _hb;
}
    void Start()
    {
        health = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
    }
    

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health,maxHealth);
        if (health <= 0.0f) 
        {
            //destroy the NetworkManager
            Destroy(GameObject.Find("NetworkManager"));
            StartCoroutine(PlayerUI.Notify("You lost !", 2f));
            Cursor.lockState = CursorLockMode.None;

            SceneManager.LoadScene("LoginScene");

        }


    }

    /// <summary>
    /// Heal le joueur (sauf si c'est points de vie sont au max)
    /// Retourne true si le joueur est heal, false sinon
    /// </summary>
    /// <param name="hp">nombres d'hp a ajouter</param>
    /// <returns></returns>
    public bool AddHealth(float hp)
    {
        if (Math.Abs(health - maxHealth) < 0.05f)
        {
            StartCoroutine(PlayerUI.Notify("Health is at maximum",2f));
            return false;
        }
        if (health + hp > maxHealth)
            health = maxHealth;
        else
            health += hp;
        healthBar.SetHealth(health,maxHealth);
        return true;
    }
}
