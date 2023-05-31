using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gaz : MonoBehaviour
{
    public float damageCoolDown;
    public float damage;
    public AudioClip[] coughAudioClips;
        
    private float currentCD;

    private void Awake()
    {
        currentCD = damageCoolDown;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentCD <= 0.0f)
            {
                other.GetComponent<Health>().TakeDamage(damage);
                other.GetComponent<AudioSource>().PlayOneShot(coughAudioClips[Random.Range(0,coughAudioClips.Length)]);
                currentCD = damageCoolDown;
                Debug.Log("Tu prend des degats");
            }
            else
            {
                Debug.Log(currentCD);
                currentCD -= Time.deltaTime; 
            }
        }
    }
    
}
