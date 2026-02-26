using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // Reference to the UI Slider component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if(!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1); // Set default volume to 1 if not already set
            Load(); // Load the saved volume setting 
        }
       else
        {
            Load();
        }
    }

    // Update is called once per frame
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value; // Set the global volume to the slider's value
        Save(); // Save the new volume setting
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume"); // Load the saved volume, default to 1 if not set
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume); // Save the current volume to PlayerPrefs
    }
}
