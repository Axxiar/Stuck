using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterController : MonoBehaviour
{
 
    public float speed = 3.0f;
    public float minDistance = 2.0f;
    
    private static bool isPlayerWhistling ;
    private GameObject[] targetsPlayers;
    //private bool istargetfound = false;
 
    //Agent de Navigation
    NavMeshAgent navMeshAgent;
 
    
 
    
    //Mémorise l'action actuelle
    public string currentAction;
 
    //Scanner pour trouver des cibles
    public TargetScanner targetScanner;
 
    //Cible
    public GameObject currentTarget;
 
    //Temps avant de perdre la cible
    public float delayLostTarget = 10f;
 
    private float timeLostTarget = 0;
 
 
    private void Awake()
    {

        //Référence NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        //Référence de Player
        //player = FindObjectOfType<Movements>().gameObject;
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
                navMeshAgent.ResetPath();
                return;   
            }


            //Est-ce que l'IA se déplace vers le joueur ?
            if (MovingToTarget(randomPlayer))
            {
                //En train de marcher

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
    
 
}