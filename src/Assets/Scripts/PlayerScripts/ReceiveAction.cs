using UnityEngine;
 
public class ReceiveAction : MonoBehaviour
{
    //Maximum de points de vie
    public int maxHitPoint = 5;
 
    //Points de vie actuels
    public int hitPoint;
 
    private void Start()
    {
        //Au début : Points de vie actuels = Maximum de points de vie
        hitPoint = maxHitPoint;
    }
 
 
    //Permet de recevoir des dommages
    public void GetDamage(int damage)
    {
        //Applique les dommages aux points de vies actuels
        hitPoint -= damage;
 
        //Si les point de vie sont inférieurs à 1 = Supprime l'objet
        if(hitPoint < 1)
        {
            Destroy(gameObject);
        }
    }
}