using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public bool isGamePaused = false;
    public bool isSceneActive = true;

    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;

    static GameManager instance;

    public Slider sensitivityX, sensivityY, SfxVolume, MusicVolume, MasterVolume;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }


    private void Awake()
    {
        sensitivityX.value = PlayerPrefs.GetFloat("SensitivityX");
        sensivityY.value = PlayerPrefs.GetFloat("SensitivityY");    
        SfxVolume.value = PlayerPrefs.GetFloat("SfxVolume");
        MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        MasterVolume.value = PlayerPrefs.GetFloat("MasterVolume");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (isGamePaused)
            {
               Resume();

            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGamePaused = true;
        OnGamePaused.Invoke();
        Time.timeScale = 0;
    }


   public void Resume()
    {
        isGamePaused = false;
        OnGameResumed.Invoke();

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
