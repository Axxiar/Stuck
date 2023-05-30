using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//stream reader and writer
using System.IO;
using System;
using TMPro;

public class SettignsMenu : MonoBehaviour
{
    //serialize fiel for tmp input field of volume
    [SerializeField] private GameObject volumeInputField;
    //serialize fiel for tmp input field of sensitivity
    [SerializeField] private GameObject sensitivityInputField;
    //serialize field for the text of the volume input field
    [SerializeField] private TMP_Text volumeText;
    //serialize field for the text of the sensitivity input field
    [SerializeField] private TMP_Text sensitivityText;


    // change volume

    private void Start()
    {
        //read from the file named volume
        StreamReader reader = new StreamReader("volume.txt");
        //get the value of the volume
        float volume = float.Parse(reader.ReadLine());
        //set the volume
        AudioListener.volume = volume/5;
        Debug.Log(volume);
        volumeText.text = volume.ToString();


        //read from the file named sensitivity
        StreamReader reader2 = new StreamReader("sensitivity.txt");
        //get the value of the sensitivity
        Int32 sensitivity = Int32.Parse(reader2.ReadLine());
        //set the sensitivity with the mouseSensitivityX parameter of the MouseLook script
        sensitivityText.text = sensitivity.ToString();



        //change the text of the volume text to the volume
        //change the text of the sensitivity text to the sensitivity

        //delete the reader
        reader.Close();
        reader2.Close();


    }

    public void enableSettingsMenu()
    {
        //enable the settings menu
        gameObject.SetActive(true);
    }

    [SerializeField] private GameObject cnvas;

    public void disableSettingsMenuM()
    {
        //disable the settings menu
        gameObject.SetActive(false);
        cnvas.SetActive(true);
    }
    
    public void enableSettM()
    {
        gameObject.SetActive(true);
        cnvas.SetActive(false);
    }

    public void disableSettingsMenu()
    {
        //disable the settings menu
        gameObject.SetActive(false);
    }

    private void Update()
    {

        ChangeVolume();
        ChangeSensitivity();
        //if the settings menu is active

    }

    public void ChangeVolume()
    {
        //get the value of the input field
        float volume;
        try
        {
            volume = float.Parse(volumeInputField.GetComponent<TMPro.TMP_InputField>().text);
        }
        catch (FormatException e)
        {
            volume = 0f;
        }
        //set the volume
        AudioListener.volume = volume/5;

        //change the text of the volume text to the volume
        volumeText.GetComponent<TMPro.TMP_Text>().text = volume.ToString();
        //write the volume in the file named volume
        StreamWriter writer = new StreamWriter("volume.txt");
        writer.WriteLine(volume);
        //delete the writer
        writer.Close();
    }

    // change sensitivity
    public void ChangeSensitivity()
    {
        //get the value of the input field
        Int32 sensitivity = Int32.Parse(sensitivityInputField.GetComponent<TMPro.TMP_InputField>().text);
        //set the sensitivity with the mouseSensitivityX parameter of the MouseLook script
        //change the text of the sensitivity text to the sensitivity
        sensitivityText.GetComponent<TMPro.TMP_Text>().text = sensitivity.ToString();
        //write the sensitivity in the file named sensitivity
        StreamWriter writer2 = new StreamWriter("sensitivity.txt");
        writer2.WriteLine(sensitivity);
        //delete the writer
        writer2.Close();
    }
}
