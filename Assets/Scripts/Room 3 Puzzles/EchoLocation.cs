using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocation : MonoBehaviour
{   
    public AudioClip echoSound;
    private AudioSource audioSource;
    public GameObject echoOBJ;

    [SerializeField] private float echoTime;
    [SerializeField] private float expansionMultiplier;
    [SerializeField] private float echoSpeed=5;

    

    private bool isEchoing;

    private void Awake()
    {
        audioSource = echoOBJ.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&!isEchoing) 
        {
            StartCoroutine(Echo());
        }
    }


    IEnumerator Echo()
    {   float timer = 0;
        isEchoing = true;
        echoOBJ.SetActive(true);
        var targetScale=Vector3.one*expansionMultiplier;
        audioSource.PlayOneShot(echoSound);
        while(echoOBJ.transform.localScale!=targetScale)
        {
            
            //echoOBJ.transform.localScale = Vector3.Lerp(echoOBJ.transform.localScale, Vector3.one * expansionMultiplier, Time.deltaTime);
            echoOBJ.transform.localScale=Vector3.MoveTowards(echoOBJ.transform.localScale, targetScale, echoSpeed);
            yield return null;
        }


        echoOBJ.SetActive(false);
        isEchoing = false;
        echoOBJ.transform.localScale = Vector3.one;
    }

}
