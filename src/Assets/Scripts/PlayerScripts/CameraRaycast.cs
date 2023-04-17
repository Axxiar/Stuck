using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float viewRange;

    RaycastHit hit;
    Ray ray;
    void Update()
    {
        Score.isFilming = false;
        for (int j = -10; j < 10; j+=5)
        {
            for (int i = -20; i < 20; i+=3)
            {
                ray = new Ray(transform.position, transform.forward * viewRange + transform.right * i + transform.up * j);
                Debug.DrawRay(transform.position, transform.forward * viewRange + transform.right * i + transform.up * j, Color.green);
                
                if (Physics.Raycast(ray, out hit, viewRange) && hit.collider.CompareTag("Enemy"))
                {
                    Score.isFilming = true;
                    return;
                }
            }
        }
    }
}
