using System.Collections;
using UnityEngine;


public class Door : MonoBehaviour {

    public float range = 7f;
    public Camera fpsCam;
    
    public AudioSource playerAS;
    public AudioClip[] doorOpenAudioClips;
    public AudioClip doorClackAudioClip;
    
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
            StartCoroutine(PlayerUI.Notify("'F' - Open", .2f));
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(OpenDoor(hit.transform.gameObject));
            }
        }
        
    }

    IEnumerator OpenDoor(GameObject targetDoor)
    {
        playerAS.PlayOneShot(doorOpenAudioClips[Random.Range(0,doorOpenAudioClips.Length)]);
        targetDoor.GetComponent<Animator>().Play("OpenDoor");
        yield return new WaitForSeconds(5.9f);
        playerAS.PlayOneShot(doorClackAudioClip);
        targetDoor.GetComponent<Animator>().Play("Wait");
    } 
}