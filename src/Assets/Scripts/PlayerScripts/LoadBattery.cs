using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBattery : MonoBehaviour
{
    public CameraBattery _cameraBattery;

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.L))
         {
             if (Inventory.BatteriesCount == 0)
                 StartCoroutine(PlayerUI.Notify("You do not have batteries", 1.5f));
             else
             {
                 if (_cameraBattery.currentBatteryPercent > 66f)
                 {
                     StartCoroutine(PlayerUI.Notify("your battery is already full ", 1.5f));
                 }
                 else
                 {
                     Inventory.BatteriesCount--;
                     if (_cameraBattery.currentBatteryPercent == 66f)
                         _cameraBattery.currentBatteryPercent = 100f;
                     else
                     {
                         _cameraBattery.currentBatteryPercent += 34f;    
                     }
                     

                 }
             }
         }
    }
}
