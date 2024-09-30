using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoDoor : MonoBehaviour
{
    public GameObject ScriptObjects;
    private GameObject light;

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
}
