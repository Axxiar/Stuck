using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour {

    public float range = 7f;
    public bool isOpening = false;
    public Camera fpsCam;
    
    private GameObject door;

    // Update is called once per frame
    void Update() {
        if (PlayerUI.GameIsPaused)
        {
            return;
        }
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.CompareTag("Doors"))
        {
            StartCoroutine(PlayerUI.Notify("'H' - Open", .2f));
            if (Input.GetKeyDown("h"))
            {
                StartCoroutine(OpenDoor(hit.transform.gameObject));
            }
        }
        
    }

    IEnumerator OpenDoor(GameObject targetDoor)
    {
        isOpening = true;
        targetDoor.GetComponent<Animator>().Play("OpenDoor");
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(5.0f);
        targetDoor.GetComponent<Animator>().Play("Wait");
        isOpening = false;
    }

}