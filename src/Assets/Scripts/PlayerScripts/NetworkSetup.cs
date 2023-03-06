using System;
using UnityEngine;
using Mirror;

public class NetworkSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;
    private Camera sceneCamera;
    private void Start()
    {
        // si le joueur n'est pas celui du client,
        // on désactive les scripts de mouvements et autres
        if (!isLocalPlayer)
        {
            Debug.Log("Not my client");
            Debug.Log(this);
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            Debug.Log("My client !");
            Debug.Log(this);
            sceneCamera = Camera.main;
            if (sceneCamera != null) sceneCamera.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (sceneCamera != null) sceneCamera.gameObject.SetActive(true);
    }
}
