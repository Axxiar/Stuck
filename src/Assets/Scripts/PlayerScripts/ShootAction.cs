using UnityEngine;
 
public class ShootAction : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;
 
    //Portée du tir
    public float weaponRange = 200f;
 
    //Force de l'impact du tir
    public float hitForce = 100f;
 
    //La caméra
    private Camera fpsCam;
 
    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;
 
    //Float : mémorise le temps du prochain tir possible
    private float nextFire;
 
    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;
 
 
    // Start is called before the first frame update
    void Start()
    {
 
        //Référence de la caméra. GetComponentInParent<Camera> permet de chercher une Camera
        //dans ce GameObject et dans ses parents.
        fpsCam = GetComponentInParent<Camera>();
    }
 
    // Update is called once per frame
    void Update()
    {
        // Vérifie si le joueur a pressé le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //Nouveau tir
 
            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + fireRate;
 
            print(nextFire);
 
            //On va lancer un rayon invisible qui simulera les balles du gun
 
            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
 
            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;
 
             
            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                print("Target");
 
                // Vérifie si la cible a un RigidBody attaché
                if (hit.rigidbody != null)
                {
 
                    //AddForce = Ajoute Force = Pousse le RigidBody avec la force de l'impact
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
 
                    //S'assure que la cible touchée a un composant ReceiveDamage
                    if (hit.collider.gameObject.GetComponent<ReceiveDamage>() != null)
                    {
                        //Envoie les dommages à la cible
                        hit.collider.gameObject.GetComponent<ReceiveDamage>().GetDamage(gunDamage);
                    }
                }
            }
        }
    }
}