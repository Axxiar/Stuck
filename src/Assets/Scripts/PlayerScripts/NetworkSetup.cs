using System;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class NetworkSetup : NetworkBehaviour

{
    [SerializeField] Behaviour[] componentsToDisable;
    private Camera sceneCamera;

    [SerializeField] private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

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
            // sinon, on lui desactive la camera principale
            sceneCamera = Camera.main;
            if (sceneCamera != null) sceneCamera.gameObject.SetActive(false);
            
            // création du UI du joueur (local uniquement)
            playerUIInstance = Instantiate(playerUIPrefab);
            HealthBar playerHB = playerUIInstance.GetComponentInChildren<HealthBar>();
            GetComponentInChildren<Health>().SetHealthBar(playerHB);
            StorageBar playerSB = playerUIInstance.GetComponentInChildren<StorageBar>(includeInactive:true);
            GetComponentInChildren<Score>().SetStorageBar(playerSB);
            
            Debug.Log(transform.name+" viens de se connecter");
            Time.timeScale = 1f;
            // GameIsPaused = false;
        }
    }

    // lorsque le script est désactivé (=joueur quitte la partie)
    private void OnDisable()
    {
        // on enlève son playerUI
        Destroy(playerUIInstance);
        
        // on lui remet la caméra principale
        if (sceneCamera != null) sceneCamera.gameObject.SetActive(true);
    }
}
