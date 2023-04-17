using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float pickupRange;
    public int maxItemCount;
    private int batteriesCount = 0;

    void Update()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, transform.forward*pickupRange, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (ItemsCount() < maxItemCount)
                    {
                        batteriesCount += 1;
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
            
        }
    }

    private int ItemsCount()
    {
        return batteriesCount;
    }
}
