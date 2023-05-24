using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattery : MonoBehaviour
{
    public static bool IsCamBatteryEmpty; 
    public float coolDownTime;
    public float currentBatteryPercent;
        
    private CamBatteryBar camBatteryBar;
    private float coolDown;
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
