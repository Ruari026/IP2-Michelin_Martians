using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public GameObject defaultScreen;
    public GameObject volumeScreen;
    public GameObject displayScreen;

    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    float soundValue;

    //this sets the audio mixer that is linked to all sounds to be equal to that of the slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    //this sets the slider equal to whatever the audio mixers value is currently set as
    private void Start()
    {
        audioMixer.GetFloat("masterVolume", out soundValue);
        volumeSlider.value = soundValue;

        audioMixer.GetFloat("sfxVolume", out soundValue);
        sfxSlider.value = soundValue;

        audioMixer.GetFloat("musicVolume", out soundValue);
        musicSlider.value = soundValue;
    }

    //Changing the Settings Screens
    public void SetSettingsScreen(string screen)
    {
        defaultScreen.SetActive(false);
        volumeScreen.SetActive(false);
        displayScreen.SetActive(false);

        if (screen == "Default")
        {
            defaultScreen.SetActive(true);
        }
        else if (screen == "Volume")
        {
            volumeScreen.SetActive(true);
        }
        else if (screen == "Display")
        {
            displayScreen.SetActive(true);
        }
    }
}
