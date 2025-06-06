using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ClownController : MonoBehaviour
{
    Animator _animator;
    private float speed ;
    public float runspeed = 10f;
    public float walkspeed = 5f;
    public AudioClip[] attackAudioClips;
    private const string walk_state = "Walk";
    private const string run_state = "Run";
    private const string attack_state = "Attack";
    private static bool isPlayerWhistling;
    private GameObject[] targetsPlayers;
    //private bool istargetfound = false;

    private AudioSource monsterAS;
    
    //Agent de Navigation
    public NavMeshAgent navMeshAgent;

    public float range = 10f;

    //Mémorise l'action actuelle
    public string currentAction;

    //Scanner pour trouver des cibles
    public TargetScanner targetScanner;

    //Cible
    public GameObject currentTarget;

    //Temps avant de perdre la cible
    public float delayLostTarget = 10f;

    // temps max que l'ia peut rester bloquée contre un mur ou escalier avant de changer de direction
    public float stuckMaxTime;

    public AudioClip targetFoundAudioClip;
    private bool hasTriggeredAudio = false;
    
    private float timeLostTarget = 0;
    private Rigidbody rb;
    private float stuckIteration = 0;
    private bool isPlayingAttackClip = false;
    private void Start()
    {
        monsterAS = GetComponent<AudioSource>();
        currentAction = walk_state;
        speed = walkspeed;
        _animator = GetComponent<Animator>();
        //Référence NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    private void Update()
    {
        targetsPlayers = GameObject.FindGameObjectsWithTag("Player");
        navMeshAgent.speed = speed;

        if (targetsPlayers.Length >= 1)
        {
            // cible choisie au hazard, à changer.
            GameObject randomPlayer = targetsPlayers[Random.Range(0, targetsPlayers.Length)];

            //Détection
            FindingTarget(randomPlayer);

            //Si pas de cible, ne fait rien
            if (currentTarget == null)
            {
                Walk();
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //done with path
                {
                    Vector3 point;
                    if (RandomPoint(navMeshAgent.transform.position, range, out point)) //pass in our centre point and radius of area
                    {
                        navMeshAgent.SetDestination(point);
                    }
                }
            }
            else
            {
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    Attack();
                }
                else
                {
                    if (!hasTriggeredAudio)
                    {
                        AudioSource playerAS;
                        foreach (GameObject player in targetsPlayers)
                        {
                            playerAS = player.GetComponent<AudioSource>();
                            if (playerAS.isPlaying)
                            {
                                StartCoroutine(WaitToPlay(playerAS,targetFoundAudioClip));
                            }
                            else
                            {
                                playerAS.PlayOneShot(targetFoundAudioClip);
                            }
                        }

                        hasTriggeredAudio = true;
                    }
                    Run();
                    MovingToTarget(randomPlayer);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Stairs"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (collisionInfo.gameObject.CompareTag("Doors"))
        {
            stuckIteration += Time.deltaTime;
            if (stuckIteration >= stuckMaxTime)
            {
                Debug.Log("Le clown était bloqué, changement de destination");
                Vector3 point;
                if (RandomPoint(navMeshAgent.transform.position, range, out point)) //pass in our centre point and radius of area
                {
                    navMeshAgent.SetDestination(point);
                }
                else
                {
                    Vector3 randomDirection = Random.insideUnitSphere * range + transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDirection, out hit, range, 1);
                    Vector3 finalPosition = hit.position;
                    navMeshAgent.SetDestination(finalPosition);
                }
                stuckIteration = 0;
            }
        }
        else if (GetComponent<Rigidbody>().isKinematic)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    
    public void DealDamage(int _dmg)
    {
        currentTarget.GetComponent<Health>().TakeDamage(_dmg);
    }
   
    
    void Walk()
    {
        ResetAnimation();
        speed = walkspeed;
        currentAction = walk_state;
        _animator.SetBool(walk_state, true);
       
    }
    void Attack()
    {
        ResetAnimation();
        currentAction = attack_state;
        _animator.SetBool(attack_state, true);
        StartCoroutine(PlayAttackClip());
    }

    private IEnumerator PlayAttackClip()
    {
        if (!isPlayingAttackClip)
        {
            isPlayingAttackClip = true;
            monsterAS.PlayOneShot(attackAudioClips[Random.Range(0,attackAudioClips.Length)]);
            yield return new WaitForSeconds(1.70f);
            isPlayingAttackClip = false;
        }
    }
    public void Run()
    {
        ResetAnimation();
        speed = runspeed;
        currentAction = run_state;
        _animator.SetBool(run_state,true);
    }

    //permet de detecter le sifflement 
    public static void SetIsPlayerWhistling(bool value)
    {
        isPlayerWhistling = value;
    }

    private IEnumerator WaitToPlay(AudioSource playerAS,AudioClip clip)
    {
        yield return new WaitUntil(() => playerAS.isPlaying == false);
        playerAS.PlayOneShot(clip);
    }
    
    private bool MovingToTarget(GameObject player)
    {

        //Assigne la destination : le joueur
        navMeshAgent.SetDestination(player.transform.position);

        //Si navMeshAgent n'est pas prêt
        if (navMeshAgent.remainingDistance == 0)
            return true;


        //Si arrivé à bonne distance, regarde vers le joueur
        RotateToTarget(currentTarget.transform);

        return false;

    }



    //Cherche une cible
    private void FindingTarget(GameObject player)
    {
        //Si le joueur est détecté (visuellement ou au sifflement)  
        if (targetScanner.Detect(transform, player) || isPlayerWhistling)
        {
            currentTarget = player;
            timeLostTarget = 0;
            SetIsPlayerWhistling(false);
            return;
        }

        //Si le joueur était détecté
        //Calcule le temps avant d'abandonner
        if (currentTarget != null)
        {
            timeLostTarget += Time.deltaTime;

            if (timeLostTarget > delayLostTarget)
            {
                hasTriggeredAudio = false;
                timeLostTarget = 0;
                currentTarget = null;
            }
            return;
        }
        currentTarget = null;
    }


    //Permet de tout le temps regarder en direction de la cible
    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    private bool RandomPoint(Vector3 center, float _range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * _range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f,
                NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ResetAnimation()
    {
        _animator.SetBool(walk_state, false);
        _animator.SetBool(run_state, false);
        _animator.SetBool(attack_state,false);
    }
}