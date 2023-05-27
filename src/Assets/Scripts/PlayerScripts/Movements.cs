using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Movements : MonoBehaviour
{
    // controller du charcter du joueur;
    public CharacterController controller;
    public Animator playerAnimator;

    // Vitesse de marche lente
    public float slowWalkSpeed = 3.0f;

    // Vitesse de marche
    public float walkSpeed = 7f;

    // Vitesse de course
    public float runSpeed = 12f;

    // Hauteur de saut
    public float jumpHeight = 7f;

    // Gravité
    // Cette valeur DOIT être négative sinon le joueur "tombe" vers le ciel
    public float gravity = -25.62f;

    // Fluidité du mouvement
    [Range(0.0f, 0.4f)] public float moveSmoothness = 0.1f;

    public AudioSource footStepAudioSource;


    Vector2 _currentDir = Vector2.zero;
    Vector2 _currentVelocity = Vector2.zero;
    private float _velocityY;
    float _currentSpeed;

    private float animHorizontal;
    private float animVertical;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        footStepAudioSource.gameObject.SetActive(false);
    }

    void Update()
    {
        // pour éviter que le joueur continue à bouger tout seul quand on ouvre un menu
        if (PlayerUI.GameIsPaused)
        {
            controller.Move(Vector3.zero);
            StopFootsteps();
            return;
        }


        // gestion des différentes vitesses de déplacement
        //fast
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = runSpeed;
            animHorizontal = 5.5f;
            animVertical = 5.5f;
        }
        //slow
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = slowWalkSpeed;
            animHorizontal = 4f;
            animVertical = 4f;
        }
        //normal
        else
        {
            _currentSpeed = walkSpeed;
            animHorizontal = 2f;
            animVertical = 2f;
        }

        // récup des directions
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        // normalisation du vecteur pour que sa longueur soit toujours 1 même en diagonal
        targetDir.Normalize();

        // smooth du déplacement (uniquement axe z et x)
        _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentVelocity, moveSmoothness);

        playerAnimator.SetFloat("X_Velocity", animHorizontal * targetDir.x);
        playerAnimator.SetFloat("Y_Velocity", animVertical * targetDir.y);

        // si saut
        // if (controller.isGrounded && Input.GetButtonDown("Jump"))
        if (controller.isGrounded)
        {
            _velocityY = Input.GetButton("Jump") ? jumpHeight : 0.0f;

            //Bruitages
            if (targetDir.x != 0 || targetDir.y != 0) // si le joueur se déplace
            {
                Footsteps();
            }
            else
            {
                StopFootsteps();
            }
        }
        // sinon on chute
        else
            _velocityY += gravity * Time.deltaTime;

        // application des mouvement sur tous les axes : x y z
        Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _currentSpeed +
                           Vector3.up * _velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void Footsteps()
    {
        if (Math.Abs(_currentSpeed - slowWalkSpeed) < 0.05f)
        {
            footStepAudioSource.gameObject.SetActive(true);
            footStepAudioSource.pitch = 0.8f;
        }
        else if (Math.Abs(_currentSpeed - walkSpeed) < 0.05f)
        {
            footStepAudioSource.gameObject.SetActive(true);
            footStepAudioSource.pitch = 1f;
        }
        else //run speed
        {
            footStepAudioSource.gameObject.SetActive(true);
            footStepAudioSource.pitch = 2f;
        }
    }

    void StopFootsteps()
    {
        if (footStepAudioSource.isPlaying)
        {
            footStepAudioSource.gameObject.SetActive(false);
        }
    }
}