using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetStorage(float data, float maxData)
    {
        slider.value = data / maxData;
    }
}
