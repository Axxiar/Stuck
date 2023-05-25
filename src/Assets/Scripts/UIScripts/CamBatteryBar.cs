
using UnityEngine;
using UnityEngine.UI;

public class CamBatteryBar : MonoBehaviour
{
    public Slider slider;


    public void SetCameraBattery(float percentage)
    {
        slider.value = percentage / 100f;
    }
}
