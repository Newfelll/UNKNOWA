using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource musicSource;
    public float blendSpeed;

    static MusicManager instance;
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PauseMusic()
    {
       StartCoroutine(FadeOut());
    }
    public void UnpauseMusic()
    {   

       StartCoroutine(FadeIn());
    
    }


    IEnumerator FadeOut()
    {
        while (musicSource.volume > 0f)
        {
            musicSource.volume -= blendSpeed * Time.deltaTime;
            if (musicSource.volume < 0)
            {
                musicSource.volume = 0f;
            }
            yield return null;
        }
        musicSource.Pause();
    }

    IEnumerator FadeIn()
    {
        musicSource.UnPause();
        while (musicSource.volume < 1)
        {
            musicSource.volume += blendSpeed * Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 1;
    }
}
