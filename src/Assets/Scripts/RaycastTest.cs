using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);

        RaycastHit hit;
        Ray cubeRay = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(cubeRay, out hit))
        {
            print(hit.collider.tag);
            Debug.Log(hit.transform.name +" traverse le rayon");
        }
    }
}
