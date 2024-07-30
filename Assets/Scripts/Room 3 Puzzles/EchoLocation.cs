using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocation : MonoBehaviour
{
    public GameObject echoOBJ;

    [SerializeField] private float echoTime;
    [SerializeField] private float expansionMultiplier;

    private bool isEchoing;

   

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
        
        while(timer<echoTime)
        {
            timer += Time.deltaTime;
            echoOBJ.transform.localScale = Vector3.Lerp(echoOBJ.transform.localScale, Vector3.one * expansionMultiplier, Time.deltaTime);
            yield return null;
        }


        echoOBJ.SetActive(false);
        isEchoing = false;
        echoOBJ.transform.localScale = Vector3.one;
    }

}
