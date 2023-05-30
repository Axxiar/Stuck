using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public static Lobby instance;

    public Unity.Services.Lobbies.Models.Lobby hostLobby;
    public Unity.Services.Lobbies.Models.Lobby joinedLobby;
    public Unity.Services.Lobbies.Models.Lobby Lobbyever;
    
    public int currentLobbyPlayerCount;
    public int joinedlobbyplayercount;
    private float heartBeatTimer;
    public int LobbyeverPC;

    [SerializeField] private GameObject lobbyCodeText;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player3;
    [SerializeField] private GameObject Player4;


    public TMP_InputField playerNameInputField;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }

    private void Update()
    {
        HandleLobbyHeartBeat();
        HandleLobbyPollUpdate();
        updatePlayers();
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
    }

    private void updatePlayers()
    {


        //get the current player count and set active the corresponding players
        if (LobbyeverPC == 1)
        {
            Player1.SetActive(true);
            Player2.SetActive(false);
            Player3.SetActive(false);
            Player4.SetActive(false);
        }
        else if (LobbyeverPC == 2)
        {
            Player1.SetActive(true);
            Player2.SetActive(true);
            Player3.SetActive(false);
            Player4.SetActive(false);
        }
        else if (LobbyeverPC == 3)
        {
            Player1.SetActive(true);
            Player2.SetActive(true);
            Player3.SetActive(true);
            Player4.SetActive(false);
        }
        else if (LobbyeverPC == 4)
        {
            Player1.SetActive(true);
            Player2.SetActive(true);
            Player3.SetActive(true);
            Player4.SetActive(true);
        }
        else
        {
            Player1.SetActive(false);
            Player2.SetActive(false);
            Player3.SetActive(false);
            Player4.SetActive(false);
        }

        //get the joined player count and set active the corresponding players
        
    }

    private async void HandleLobbyHeartBeat()
    {
        if (hostLobby == null)
        {
            return;
        }

        heartBeatTimer -= Time.deltaTime;
        if (heartBeatTimer > 0f)
        {
            return;
        }

        float heartBeatTimerMax = 15;
        heartBeatTimer = heartBeatTimerMax;

        await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
    }

    private async void HandleLobbyPollUpdate()
    {
        if (Lobbyever == null)
        {
            return;
        }

        heartBeatTimer -= Time.deltaTime;
        if (heartBeatTimer > 0f)
        {
            return;
        }

        float heartBeatTimerMax = 1.1f;
        heartBeatTimer = heartBeatTimerMax;


        try
        {
            Lobbyever = await LobbyService.Instance.GetLobbyAsync(Lobbyever.Id);
            LobbyeverPC = Lobbyever.Players.Count; ;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


    public void ChangeLobbyCodeText()
    {

        lobbyCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = hostLobby.LobbyCode;
    }

    public async void CreateLobby(string lobbyName)
    {
        try
        {
            int maxPlayers = 4;
            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = false;


            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            hostLobby = lobby;
            Lobbyever = lobby;

            ChangeLobbyCodeText();

            
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        

    }






    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;
            options.Order = new List<QueryOrder>()
            {
            new QueryOrder(
            asc: false,
            field: QueryOrder.FieldOptions.Created)
            };
            QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);

            foreach (Unity.Services.Lobbies.Models.Lobby l in lobbies.Results)
            {
                Debug.Log(l.Name );
            }
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyByCode(TMP_InputField lobbyCode)
    {
       try
       {
            joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode.text);

            lobbyCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = joinedLobby.LobbyCode;

            Lobbyever = joinedLobby;

        }
        catch (LobbyServiceException e)
       {
            Debug.Log(e);
       }
    }

    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();

        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
            await LobbyService.Instance.RemovePlayerAsync(hostLobby.Id, AuthenticationService.Instance.PlayerId);
            hostLobby = null;
            joinedLobby = null;
            Lobbyever = null;

        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    
}
