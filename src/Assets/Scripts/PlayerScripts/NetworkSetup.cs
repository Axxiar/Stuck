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
            
            // !!!!! IMPORTANT !!!
            // ici le seul moyen que j'ai trouvé de récupérer sur l'ui du player (cf. le prefab PlayerUI) le text qui sert
            // à afficher les notifs, c'est de faire comme ci-dessous. Attention au nombre 7 il peut causer des erreurs si on 
            // bidouille l'ui.
            // Le 7 correspond au 7ème élément dans le prefab "PlayerUI" c'est à dire (si tout est en ordre) 
            playerUIInstance.GetComponent<PlayerUI>().SetNotification(playerUIInstance.transform.GetChild(7).gameObject);
            // !!!!!!!!!!!!!!!!!!!
            
            HealthBar playerHB = playerUIInstance.GetComponentInChildren<HealthBar>();
            GetComponentInChildren<Health>().SetHealthBar(playerHB);
            StorageBar playerSB = playerUIInstance.GetComponentInChildren<StorageBar>(includeInactive:true);
            GetComponentInChildren<Score>().SetStorageBar(playerSB);
            
            // il y a 2 éléments CamBatteryBar dans l'ui, le premier se situe dans le menue Pause, le second est dans l'ui de la caméra. 
            // Ici, on veut récupérer seulement le 2ème (celui de l'ui de la caméra)
            CamBatteryBar playerCBB = playerUIInstance.GetComponentsInChildren<CamBatteryBar>(includeInactive: true)[1];
            GetComponentInChildren<CameraBattery>().SetCamBatteryBar(playerCBB);
            
            
            // on lui donne son nom
            string username = UserAccountManager.LoggedInUsername;
            CmdSetUsername(transform.name, username);

            Debug.Log(username + " viens de se connecter");
        }
    }

    [Command]
    private void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + " a rejoint la partie");
            player.username = username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // on récupère l'id du joueur et on l'ajoute à la liste des joueurs
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }


    // lorsque le script est désactivé (=joueur quitte la partie)
    private void OnDisable()
    {
        // on enlève son playerUI
        Destroy(playerUIInstance);
        
        // on lui remet la caméra principale
        if (sceneCamera != null) sceneCamera.gameObject.SetActive(true);

        // on enlève le joueur de la liste des joueurs
        GameManager.UnRegisterPlayer(transform.name);
    }
}
