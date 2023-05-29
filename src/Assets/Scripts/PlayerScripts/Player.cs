using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string username = "Player";

    [SyncVar]
    public float health = 100f;

    void update()
    {
        health = GetComponent<Health>().health;
    }
}
