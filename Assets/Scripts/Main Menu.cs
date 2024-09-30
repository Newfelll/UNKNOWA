using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider SfxVolume, MusicVolume, MasterVolume, SensitivityX, SensitivityY;


    private void Start()
    {
        SfxVolume.value = PlayerPrefs.GetFloat("SfxVolume");
        MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        MasterVolume.value = PlayerPrefs.GetFloat("MasterVolume");
        SensitivityX.value = PlayerPrefs.GetFloat("SensitivityX");
        SensitivityY.value = PlayerPrefs.GetFloat("SensitivityY");
    }

}
