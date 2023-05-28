using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{


    private Unity.Services.Lobbies.Models.Lobby hostLobby;
    private float heartBeatTimer;

    public InputField playerName;


    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        CreateLobby("f");
        ListLobbies();
    }

    private void Update()
    {
        
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


    public async void CreateLobby(string lobbyName)
    {
        try
        {
            int maxPlayers = 4;
            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = false;


            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            hostLobby = lobby;

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

    private async void JoinLobbyByCode(string lobbyCode)
    {
       try
       {
            Unity.Services.Lobbies.Models.Lobby joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);

       } catch (LobbyServiceException e)
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

    
}
