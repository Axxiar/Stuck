using System;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;
    private Camera mainCamera;
    private void Start()
    {
        // si le joueur n'est pas celui du client,
        // on désactive les scripts de mouvements et autres
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            mainCamera = Camera.main;
            if (mainCamera != null) mainCamera.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (mainCamera != null) mainCamera.gameObject.SetActive(true);
    }
}
