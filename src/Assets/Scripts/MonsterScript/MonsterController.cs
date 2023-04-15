using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterController : MonoBehaviour
{
    Animator _animator;
    public float speed = 10f;
    public float minDistance = 2.0f;
    
    private static bool isPlayerWhistling ;
    private GameObject[] targetsPlayers;
    //private bool istargetfound = false;

    //Agent de Navigation
    public NavMeshAgent navMeshAgent;

    public float range= 10f;

    //Mémorise l'action actuelle
    public string currentAction;
 
    //Scanner pour trouver des cibles
    public TargetScanner targetScanner;
 
    //Cible
    public GameObject currentTarget;
 
    //Temps avant de perdre la cible
    public float delayLostTarget = 10f;
 
    private float timeLostTarget = 0;
 
 
    private void Start()
    {
        
        //Référence NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
 
    private void Update()
    {
        targetsPlayers = GameObject.FindGameObjectsWithTag("Player");
     
        if (targetsPlayers.Length >= 1)
        {
            // cible choisie au hazard, à changer.
            GameObject randomPlayer = targetsPlayers[Random.Range(0, targetsPlayers.Length)];
            
            //Détection
            FindingTarget(randomPlayer);
     
            //Si pas de cible, ne fait rien
            if (currentTarget == null)
            {
                if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //done with path
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
                MovingToTarget(randomPlayer);
            }
        }
        
        
    }
    //permet de detecter le sifflement 
    public static void SetIsPlayerWhistling(bool value)
    {
        isPlayerWhistling = value;
        
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
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

   
}