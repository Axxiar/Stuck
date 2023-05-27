using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float open = 100f;
    public float range = 10f;

    public GameObject door;
    public bool isOpening = false;

    public Camera fpsCam;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("h"))
        {
            Shoot();
        }
    }

    void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        isOpening = true;
        door.GetComponent<Animator>().Play("OpenDoor");
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(5.0f);
        door.GetComponent<Animator>().Play("Wait");
        isOpening = false;
    }

}