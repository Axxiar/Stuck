
using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class Inventory : MonoBehaviour
{
    public GameObject battery;
    
    public float pickupRange;
    public int maxItemCount;
    
    private int batteriesCount = 0;

    void Update()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, transform.forward*pickupRange, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange) && hit.transform.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (batteriesCount < maxItemCount)
                {
                    batteriesCount += 1;
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    StartCoroutine(PlayerUI.Alert("You already have 5 batteries", 1.5f));
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && batteriesCount > 0)
            {
                batteriesCount -= 1;
                GameObject batt = Instantiate(battery, transform.position + transform.forward * 2, Quaternion.Euler(Random.value * 180, 0, Random.value * 180));
                batt.GetComponent<Rigidbody>().AddForce(transform.forward * 5 ,ForceMode.Impulse);
            }
        }
    }
}
