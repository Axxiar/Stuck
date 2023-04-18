using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject playerTabPrefab;

    [SerializeField]
    private Transform playerTabParent;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //recuperer les joueurs
        Player[] players = GameManager.GetAllPlayers();
        //pour chaque joueur on affiche son nom
        foreach (Player player in players)
        {
            GameObject item = Instantiate(playerTabPrefab, playerTabParent);
            PlayerItem TabItem = item.GetComponent<PlayerItem>();
            if (TabItem != null)
            {
                //uses the method setup from the TextModifier script
                Debug.Log("player name: " + player.username);
            }
        }

    }
}
