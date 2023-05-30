using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattery : MonoBehaviour
{
    public static bool IsCamBatteryEmpty; 
    public static float CurrentBatteryPercent;
        
    public float coolDownTime;
    
    private CamBatteryBar camBatteryBar;
    private float coolDown;
    public void SetCamBatteryBar(CamBatteryBar _cbb)
    {
        camBatteryBar = _cbb;
    }

    void Start()
    {
        coolDown = coolDownTime;
        CurrentBatteryPercent = 100f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Inventory.BatteriesCount == 0)
                StartCoroutine(PlayerUI.Notify("You do not have batteries", 2f));
            else
            {
                if (CurrentBatteryPercent > 66f)
                {
                    StartCoroutine(PlayerUI.Notify("Your battery is already full ", 2f));
                }
                else
                {
                    IsCamBatteryEmpty = false;
                    Inventory.BatteriesCount--;
                    if (Math.Abs(CurrentBatteryPercent - 66f) < 0.01f)
                        CurrentBatteryPercent = 100f;
                    else
                    {
                        CurrentBatteryPercent += 34f;    
                    }
                    camBatteryBar.SetCameraBattery(CurrentBatteryPercent);
                }
            }
        }
        if (PlayerUI.IsCameraOn)
        {
            if (coolDown <= 0.0f)
            {
                CurrentBatteryPercent -= 34f;
                camBatteryBar.SetCameraBattery(CurrentBatteryPercent);
                
                if (CurrentBatteryPercent <= 0.0f)
                {
                    StartCoroutine(PlayerUI.Notify("Camera is discharged", 2f));
                    
                    IsCamBatteryEmpty = true;
                }
                coolDown = coolDownTime;
            }
            else
                coolDown -= Time.deltaTime; 
        }
    }
}
