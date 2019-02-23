﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    float soundValue;

    //this sets the audio mixer that is linked to all sounds to be equal to that of the slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    //this sets the slider equal to whatever the audio mixers value is currently set as
    private void Start()
    {
        audioMixer.GetFloat("masterVolume", out soundValue);
        volumeSlider.value = soundValue;
    }
}
