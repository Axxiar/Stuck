using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Propriété transform du joueur dont c'est la caméra
    public Transform playerBody;
    // Sensibilité verticale de la souris
    public float mouseYSensivity = 350.0f;
    // Sensibilité horizontale de la souris
    public float mouseXSensivity = 300.0f;
    // Valeur de la rotation sur l'axe X (haut en bas)
    private float _xRotation;

    void Start()
    {
        // On bloque le curseur (+rend invisible au passage)
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Récup de la position de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensivity * Time.deltaTime;

        // update de la rotation verticale (-= car l'axe est inversé par défaut)
        _xRotation -= mouseY;
        // bloque la rotation verticale entre -90 et 90
        _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        
        // update des rotations
        transform.localRotation = Quaternion.Euler(_xRotation, 0f ,0f); // ici on fait tourner la caméra
        playerBody.Rotate(Vector3.up * mouseX); // ici le joueur directement (quand il regarde à gauche/droite, ça fait tourner son corps directement)
    }
}
