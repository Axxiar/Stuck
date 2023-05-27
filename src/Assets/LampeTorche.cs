using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeTorche : MonoBehaviour
{

    public AudioSource playerAS;
    public AudioClip switchClipOn;
    public AudioClip switchClipOff;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GetComponent<Light>().enabled)
            {
                playerAS.PlayOneShot(switchClipOff);
            }
            else
            {
                playerAS.PlayOneShot(switchClipOn);
            }
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
