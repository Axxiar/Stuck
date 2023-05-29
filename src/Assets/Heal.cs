using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float hpGiven;
    public AudioClip healAudioClip;
    private void OnTriggerEnter(Collider hit)
    {
        GameObject touchedGO = hit.gameObject;
        if (touchedGO.CompareTag("Player"))
        {
            Health playerHealth = touchedGO.GetComponent<Health>();
            if (playerHealth.AddHealth(hpGiven))
            {
                touchedGO.GetComponent<AudioSource>().PlayOneShot(healAudioClip);
                Destroy(gameObject);
            }
            
        }
    }
}
