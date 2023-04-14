using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float viewRange;
    
    RaycastHit hit;
    Ray cubeRay;
    void Update()
    {
        cubeRay = new Ray(transform.position, transform.forward * -1);
        Debug.DrawRay(transform.position, transform.forward * -viewRange, Color.red);

        if (Physics.Raycast(cubeRay, out hit, viewRange) && hit.collider.CompareTag("Enemy"))
        {
            print(hit.collider.tag);
            Debug.Log(hit.transform.name +" traverse le rayon");
        }
    }
}
