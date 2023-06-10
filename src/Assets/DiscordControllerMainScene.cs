using System;
using System.Collections;
using System.Collections.Generic;
using Discord;
using UnityEngine;

public class DiscordControllerMainScene : MonoBehaviour
{
    public Discord.Discord discord;
    
    // Start is called before the first frame update
    void Start()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        discord = new Discord.Discord(1115452215939846274,(UInt64)Discord.CreateFlags.Default);

        ActivityManager activityManager = discord.GetActivityManager();
        string state;
        state = playerCount == 1 ? "Playing solo" : "Playing with friends";
        Activity activity = new Discord.Activity
        {
            State = state,
            Details =  "github.com/Axxiar/Stuck",
            Assets =
            {
                LargeImage = "logo",
                LargeText = "Greetings from devs :D"
            },
            Party =
            {
                Size =
                {
                    CurrentSize = 1,
                    MaxSize = 4
                }
            }
        };
        
        activityManager.UpdateActivity(activity, result =>
        {
            if (result != Result.Ok) Debug.LogWarning("Failed to update Discord Rich Presence");
            else Debug.Log("Discord Rich Presence updated successfully !");
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
