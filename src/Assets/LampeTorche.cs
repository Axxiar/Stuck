using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeTorche : MonoBehaviour
{
    public GameObject light;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOn = !isOn;
        }

        light.SetActive(isOn);
        
    }
}
