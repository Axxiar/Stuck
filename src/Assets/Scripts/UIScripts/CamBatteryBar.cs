
using UnityEngine;
using UnityEngine.UI;

public class CamBatteryBar : MonoBehaviour
{
    public Slider slider;


    public void SetCameraBattery(float percentage)
    {
        Debug.Log("Camera battery is at " + percentage/100f);
        slider.value = percentage / 100f;
    }
}
