using System;

using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Propriété transform du joueur dont c'est la caméra
    public Transform playerBody;
    // Sensibilité verticale de la souris
    public float mouseYSensivity = 100.0f;
    // Sensibilité horizontale de la souris
    public float mouseXSensivity = 70.0f;
    // 
    [Range(0.0f, 0.1f)] public float mouseSmoothness;

    public Transform torch;
    
    // Valeur de la rotation sur l'axe X (haut en bas)
    private float _xRotation;
    //
    Vector2 _currentMouseDelta = Vector2.zero;
    Vector2 _currentMouseDeltaVelocity = Vector2.zero;

    
    void Start()
    {
        // On bloque le curseur (+rend invisible au passage)
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // pour éviter que la souris continue à bouger toute seule quand on ouvre un menu
        if (PlayerUI.GameIsPaused)
        {
            playerBody.Rotate(Vector3.zero);
            return;
        }
        // Récup de la position de la souris
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        // smooth de la souris
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, mouseSmoothness);
        
        // update de la rotation verticale (-= car l'axe est inversé par défaut)
        _xRotation -= _currentMouseDelta.y;
        // bloque la rotation verticale entre -90 et 90
        _xRotation = Math.Clamp(_xRotation, -70f, 70f);
        
        // update des rotations
        transform.localRotation = Quaternion.Euler(_xRotation, 0f ,0f); // ici on fait tourner la caméra (axe vertical)
        torch.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // rotation de la torche (axe vertical)

        playerBody.Rotate(Vector3.up * _currentMouseDelta.x); // ici le joueur directement (quand il regarde à gauche/droite, ça fait tourner son corps directement)
    }
}