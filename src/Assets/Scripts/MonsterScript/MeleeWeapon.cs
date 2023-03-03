using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MeleeWeapon : MonoBehaviour
{
    //Damage que fait l'arme
    public int damage = 1;
 
    //Détermine quel Layer on peut toucher
    public LayerMask layerMask;
 
    //Est-ce que l'arme est en train d'être utilisée ?
    public bool isAttacking = false;
 
 
    public void StartAttack()
    {
        isAttacking = true;
    }
 
    public void StopAttack()
    {
        isAttacking = false;
    }
 
    //Quand MeleeWeapon entre en collision avec objet
    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking)
            return;
 
        if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
            return;
 
        //Fait des dommages au GameObject qu'on a touché
        other.GetComponent<ReceiveDamage>().GetDamage(damage);
 
    }
}