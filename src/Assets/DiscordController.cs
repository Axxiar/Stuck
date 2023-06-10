using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    
    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(1115452215939846274,(UInt64)Discord.CreateFlags.Default);

        ActivityManager activityManager = discord.GetActivityManager();
        Activity activity = new Discord.Activity
        {
            State = "In the lobby",
            Details =  "github.com/Axxiar/Stuck",
            Assets =
            {
                LargeImage = "logo",
                LargeText = "Greetings from devs :D"
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
