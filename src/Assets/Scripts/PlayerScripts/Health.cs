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



    public string GetHealth()
    {
        return health.ToString();
    }
}
