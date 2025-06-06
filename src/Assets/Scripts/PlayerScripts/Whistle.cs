using UnityEngine;

public class Whistle : MonoBehaviour
{
    public AudioClip[] whistleClips;
    public float whistleCoolDown = 10f;

    private AudioSource audioSource;
    private float currentWhistleCD;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !audioSource.isPlaying && currentWhistleCD <= 0)
        {
            int randomIndex = Random.Range(0, whistleClips.Length);
            audioSource.PlayOneShot(whistleClips[randomIndex]);
            currentWhistleCD = whistleCoolDown;
            MonsterController.SetIsPlayerWhistling(true);
            ClownController.SetIsPlayerWhistling(true);
        }

        if (currentWhistleCD > 0)
            currentWhistleCD -= Time.deltaTime;
    }
}
