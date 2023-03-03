using UnityEngine;
using UnityEngine.AI;
 
public class MonsterController : MonoBehaviour
{
    public GameObject player;
 
    public MeleeWeapon meleeWeapon;
 
    //Agent de Navigation
    NavMeshAgent navMeshAgent;
 
    //Composants
    Animator animator;
 
    //Actions possibles
 
    //Stand ou Idle = attendre
    const string STAND_STATE = "Stand";
 
    //Reçoit des dommages
    const string TAKE_DAMAGE_STATE = "Damage";
 
    //Est vaincu
    public const string DEFEATED_STATE = "Defeated";
 
    //Est en train de marcher
    public const string WALK_STATE = "Walk";
 
    //Attaque
    public const string ATTACK_STATE = "Attack";
 
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
        //Au départ, la créature attend en restant debout
        currentAction = STAND_STATE;
 
        //Référence vers l'Animator
        animator = GetComponent<Animator>();
 
        //Référence NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        //Référence de Player
        player = FindObjectOfType<Movements>().gameObject;
    }
 
    private void Update()
    {
 
        //si la créature est défaite
        //Elle ne peut rien faire d'autres
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return;
        }
 
 
        //Si la créature reçoit des dommages:
        //Elle ne peut rien faire d'autres.
        //Cela servira quand on améliorera ce script.
        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
 
 
        //Détection
        FindingTarget();
 
        //Si pas de cible, ne fait rien
        if (currentTarget == null)
        {
            //Defaut
            Stand();
            navMeshAgent.ResetPath();
            return;
        }
 
 
        //Est-ce que l'IA se déplace vers le joueur ?
        if (MovingToTarget())
        {
            //En train de marcher
            return;
        }
 
 
        //Attaque
        if (currentAction != ATTACK_STATE && currentAction != TAKE_DAMAGE_STATE)
        {
            Attack();
            return;
        }
        if (currentAction == ATTACK_STATE)
        {
            Attacking();
            return;
        }
 
 
 
    }
 
    //La créature attend
    private void Stand()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Stand"
        currentAction = STAND_STATE;
        //Le paramètre "Stand" de l'animator = true
        animator.SetBool("Stand", true);
    }
 
    public void TakeDamage()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Damage"
        currentAction = TAKE_DAMAGE_STATE;
        //Le paramètre "Damage" de l'animator = true
        animator.SetBool("Damage", true);
    }
 
    public void Defeated()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Defeated"  
        currentAction = DEFEATED_STATE;
        //Le paramètre "Defeated" de l'animator = true
        animator.SetBool(DEFEATED_STATE, true);
    }
 
 
    //Permet de surveiller l'animation lorsque l'on prend un dégât
    private void TakingDamage()
    {
 
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {
            //Compte le temps de l'animation
            //normalizedTime : temps écoulé nomralisé (de 0 à 1).
            //Si normalizedTime = 0 => C'est le début.
            //Si normalizedTime = 0.5 => C'est la moitié.
            //Si normalizedTime = 1 => C'est la fin.
 
 
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
 
 
            //Fin de l'animation
            if (normalizedTime > 1)
            {
                Stand();
            }
 
        }
 
    }
 
    private void Attacking()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_STATE))
        {
            //Compte le temps de l'animation
            //normalizedTime : temps écoulé nomralisé (de 0 à 1).
            //Si normalizedTime = 0 => C'est le début.
            //Si normalizedTime = 0.5 => C'est la moitié.
            //Si normalizedTime = 1 => C'est la fin.
 
 
 
 
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
 
 
            //Fin de l'animation
            if (normalizedTime > 1)
            {
 
                meleeWeapon.StopAttack();
                Stand();
                return;
            }
 
            meleeWeapon.StartAttack();
 
        }
    }
 
 
    private bool MovingToTarget()
    {
 
        //Assigne la destination : le joueur
        navMeshAgent.SetDestination(player.transform.position);
 
        //Si navMeshAgent n'est pas prêt
        if (navMeshAgent.remainingDistance == 0)
            return true;
 
 
        // navMeshAgent.remainingDistance = distance restante pour atteindre la cible (Player)
        // navMeshAgent.stoppingDistance = à quelle distance de la cible l'IA doit s'arrêter 
        // (exemple 2 m pour le corps à sorps) 
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
 
            if (currentAction != WALK_STATE)
                Walk();
 
        }
        else
        {
            //Si arrivé à bonne distance, regarde vers le joueur
            RotateToTarget(currentTarget.transform);
 
            return false;
        }
 
        return true;
    }
 
    //Cherche une cible
    private void FindingTarget()
    {
        //Si le joueur est détecté
        if (targetScanner.Detect(transform, player))
        {
            currentTarget = player;
            timeLostTarget = 0;
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
 
    //Walk = Marcher
    private void Walk()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Walk"
        currentAction = WALK_STATE;
        //Le paramètre "Walk" de l'animator = true
        animator.SetBool(WALK_STATE, true);
    }
 
 
    private void Attack()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Attack"
        currentAction = ATTACK_STATE;
        //Le paramètre "Attack" de l'animator = true
        animator.SetBool(ATTACK_STATE, true);
    }
 
    //Permet de tout le temps regarder en direction de la cible
    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
 
    //Réinitialise les paramètres de l'animator
    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(TAKE_DAMAGE_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }
 
}