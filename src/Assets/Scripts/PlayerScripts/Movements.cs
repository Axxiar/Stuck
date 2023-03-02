using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movements : MonoBehaviour
{
    // controller du charcter du joueur;
    public CharacterController controller;

    // Vitesse de marche lente
    public float slowWalkSpeed = 3.0f;
    // Vitesse de marche
    public float walkSpeed = 7f;
    // Vitesse de course
    public float runSpeed = 15f;
    // Hauteur de saut
    public float jumpHeight = 8f;
    // Gravité
    // Cette valeur DOIT être négative sinon le joueur "tombe" vers le ciel
    public float gravity = -19.62f;
    
    // Fluidité du mouvement
    [Range(0.0f, 0.4f)] public float moveSmoothness = 0.1f;

    Vector2 _currentDir = Vector2.zero;
    Vector2 _currentVelocity = Vector2.zero;
    private float _velocityY;
    float _currentSpeed;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // gestion des différentes vitesses de déplacement
        //fast
        if (Input.GetKey(KeyCode.LeftShift))
            _currentSpeed = runSpeed;
        //slow
        else if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = slowWalkSpeed;
        //normal
        else
            _currentSpeed = walkSpeed;
        
        // récup des directions
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        // normalisation du vecteur pour que sa longueur soit toujours 1 même en diagonal
        targetDir.Normalize();

        // smooth du déplacement (uniquement axe z et x)
        _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentVelocity, moveSmoothness);

        // si saut
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            _velocityY = jumpHeight;
        // sinon on chute
        else
            _velocityY += gravity * Time.deltaTime;

        // application des mouvement sur tous les axes : x y z
        Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _currentSpeed + Vector3.up * _velocityY;
        controller.Move(velocity * Time.deltaTime);
    }
}
