using UnityEngine;
using UnityEngine.Serialization;

public class Movements : MonoBehaviour
{
    // controller du charcter du joueur;
    public CharacterController controller;

    // Vitesse de marche
    public float walkingSpeed = 10f;

    // Vitesse de course
    public float runningSpeed = 15f;

    //Vitesse de saut
    public float jumpSpeed = 8f;

    //Gravité
    public float gravity = 20f;

    // Déplacement
    Vector3 _moveDirection;

    // booléen marche ou court ?
    bool _isRunning;


    void Update()
    {
        // récup des directions
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        // normalisation du vecteur pour que sa longueur soit toujours 1 même en diagonal
        inputDir.Normalize();
        
        
    }
}
