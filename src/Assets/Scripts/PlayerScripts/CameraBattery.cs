using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattery : MonoBehaviour
{
    public static bool IsCamBatteryEmpty; 
    public float coolDownTime;
        
    private CamBatteryBar camBatteryBar;
    private float coolDown;
    private float currentBatteryPercent;
    public void SetCamBatteryBar(CamBatteryBar _cbb)
    {
        camBatteryBar = _cbb;
    }

    void Start()
    {
        coolDown = coolDownTime;
        currentBatteryPercent = 100f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Inventory.BatteriesCount == 0)
                StartCoroutine(PlayerUI.Notify("You do not have batteries", 2f));
            else
            {
                if (currentBatteryPercent > 66f)
                {
                    StartCoroutine(PlayerUI.Notify("Your battery is already full ", 2f));
                }
                else
                {
                    IsCamBatteryEmpty = false;
                    Inventory.BatteriesCount--;
                    if (Math.Abs(currentBatteryPercent - 66f) < 0.01f)
                        currentBatteryPercent = 100f;
                    else
                    {
                        currentBatteryPercent += 34f;    
                    }
                    camBatteryBar.SetCameraBattery(currentBatteryPercent);
                }
            }
        }
        if (PlayerUI.IsCameraOn)
        {
            if (coolDown <= 0.0f)
            {
                currentBatteryPercent -= 34f;
                camBatteryBar.SetCameraBattery(currentBatteryPercent);
                
                if (currentBatteryPercent <= 0.0f)
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
