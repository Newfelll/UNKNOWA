using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsData : MonoBehaviour
{
    

    private static SettingsData instance;
    public AudioMixer sfxMixer;
    public int volumeMultiplier=30;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        

        if (instance!=null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        if(PlayerPrefs.GetFloat("FirstRun")==0)
        {
            PlayerPrefs.SetFloat("FirstRun", 1);
            PlayerPrefs.SetFloat("SensitivityX", 120f);
            PlayerPrefs.SetFloat("SensitivityY", 120f);
            PlayerPrefs.SetFloat("MusicVolume", 0.9f);
            PlayerPrefs.SetFloat("SfxVolume", 0.9f);
            PlayerPrefs.SetFloat("MasterVolume", 0.5f);
        }
        PlayerPrefs.SetFloat("FirstRun", 1);

       

      
    }

    private void Start()
    {
        instance.sfxMixer.SetFloat("sfx", Mathf.Log10(GetSfxVolume()) * volumeMultiplier);
       
        instance.sfxMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * volumeMultiplier);
        instance.sfxMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * volumeMultiplier);
    }


    public static void SetSensitivityY(float y)
    {   
        
        PlayerPrefs.SetFloat("SensitivityY", y);

        
        PlayerLook.sensY = y;
    }

    public static void SetSensitivityX(float x)
    {
        PlayerPrefs.SetFloat("SensitivityX", x);
        

        PlayerLook.sensX = x;
       
    }

    public static float GetSensitivityX()
    {
        return PlayerPrefs.GetFloat("SensitivityX");
    }

    public static float GetSensitivityY()
    {
        return PlayerPrefs.GetFloat("SensitivityY");
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat("SfxVolume");
    }



    public void SetSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
        sfxMixer.SetFloat("sfx", Mathf.Log10(volume)*volumeMultiplier);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        sfxMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * volumeMultiplier);
    }


    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        sfxMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * volumeMultiplier);
    }
}
