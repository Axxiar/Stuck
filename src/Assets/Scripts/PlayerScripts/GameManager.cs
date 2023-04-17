using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //don't destroy OnLoad the gameManager onstart
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



    //dic of all the players in the game
    public static Dictionary<string, Player> players = new Dictionary<string, Player>();

    //register all the players in the game
    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = "Player" + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    //unregister one player
    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    //get a player by his id
    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }
}
