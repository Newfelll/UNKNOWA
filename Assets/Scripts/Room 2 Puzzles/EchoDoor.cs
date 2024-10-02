using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EchoDoor : MonoBehaviour
{
    public GameObject ScriptObjects;
    private GameObject light;
    public GameObject tutorialText;

     void Awake()
    {   
        light = GameObject.FindGameObjectWithTag("Light");
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            ScriptObjects.SetActive(true);
           light.SetActive(false);
            RenderSettings.ambientIntensity= 0;
            RenderSettings.reflectionIntensity = 0;
            StartCoroutine(TutorialText());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           ScriptObjects.SetActive(false);
           light.SetActive(true);
           RenderSettings.ambientIntensity = 2.64f;
           RenderSettings.reflectionIntensity = 1f;

        }
    }

    IEnumerator TutorialText()
    {
        tutorialText.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorialText.SetActive(false);
    }
}
