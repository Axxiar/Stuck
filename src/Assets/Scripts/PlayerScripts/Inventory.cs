
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ce script est un peu trompeur, il ne gère que le ramassage et "lachage" des piles.
/// Pour l'affichage de l'inventaire, jetez un oeuil dans le script PlayerUI. 
/// </summary>
public class Inventory : MonoBehaviour
{
    public GameObject battery;
    
    public float pickupRange;
    public int maxItemCount;
    
    public static int BatteriesCount = 0;

    void Update()
    {
        if (PlayerUI.GameIsPaused)
        {
            return;
        }
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, transform.forward*pickupRange, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange) && hit.transform.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (BatteriesCount < maxItemCount)
                {
                    BatteriesCount += 1;
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && BatteriesCount > 0)
            {
                BatteriesCount -= 1;
                GameObject batt = Instantiate(battery, transform.position + transform.forward * 2, Quaternion.Euler(Random.value * 180, 0, Random.value * 180));
                batt.GetComponent<Rigidbody>().AddForce(transform.forward * 5 ,ForceMode.Impulse);
            }
        }
    }
}
