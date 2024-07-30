using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundManager : MonoBehaviour
{
    static SFXSoundManager instance;
    public static SFXSoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SFXSoundManager>();
            }
            return instance;
        }
    }

    private AudioSource audioSource;


    public AudioClip correctSFX;
    public AudioClip buttonSFX;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
  

    



    public void PlayCorrectSFX()
    {
        audioSource.PlayOneShot(correctSFX);
    }

    public void PlayButtonSFX()
    {
          audioSource.PlayOneShot(buttonSFX);
    }
}
