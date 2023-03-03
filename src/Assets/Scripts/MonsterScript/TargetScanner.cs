using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//Serializable
[System.Serializable]
public class TargetScanner //IMPORTANT ne dérive pas de MonoBehaviour
{
    //Rayon de détection autour de la créature
    public float detectionRadius = 20;
 
    //Angle de détection, pour simuler le champ de vision
    [Range(0.0f, 360.0f)]
    public float detectionAngle = 270;
 
    //Obsacle qui bloque le champ de vision
    public LayerMask obstacleLayerMask;
 
    public bool Detect(Transform detector, GameObject target)
    {
        //Calcule de la distance entre detector et target
        Vector3 distanceToTarget = target.transform.position - detector.position;
 
        //Direction normalisée
        Vector3 directionToTarget = distanceToTarget.normalized;
 
        //Si le carré de la distance est plus grand que le carré du rayon :
        //target est trop loin
        //(sqrMagnitude est plus performant que magnitude)
        if (distanceToTarget.sqrMagnitude > detectionRadius * detectionRadius)
            return false;
 
        //Si target est dans le champ de vision
        if (Vector3.Dot(directionToTarget, detector.forward) >
                   Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
        {
            //Dessine un trait bleu sur la Scene
            Debug.DrawRay(detector.position, distanceToTarget, Color.blue);
 
            //Si pas d'obstacle, target est détectée
            if (!Physics.Raycast(detector.position, directionToTarget, detectionRadius, obstacleLayerMask))
                return true;
 
        }
 
        //Défaut, rien n'est détecté
        return false;
    }
}