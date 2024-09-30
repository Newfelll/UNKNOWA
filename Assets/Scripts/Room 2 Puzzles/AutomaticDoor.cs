using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AutomaticDoor : MonoBehaviour
{
    


    Vector3 initialPosition;
    public float animTime = 3f;
    public float animDelay = 1f;
    public bool isOpen;
    private AudioSource audioSource;

    public AudioClip open, close;


    private void Awake()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        if (isOpen)
        {
            StartCoroutine(CloseDoor());
        }else
        {
            StartCoroutine(OpenDoor());
        }
    }
    IEnumerator CloseDoor()
    {   
        Vector3 initialPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y -4, transform.position.z);
        
        float timer = 0;
        audioSource.PlayOneShot(close);
        while (timer<animTime)
        {
            transform.position = Vector3.Lerp(initialPos, targetPos, timer/animTime);
            yield return null;
            timer+=Time.deltaTime;
        }
        transform.position = targetPos;
        timer = 0;
        while (timer<animDelay)
        {
            
            yield return null;
            timer += Time.deltaTime;
        }
        
       StartCoroutine(OpenDoor());
    }


    IEnumerator OpenDoor()
    {
        Vector3 initialPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        float timer = 0;
        audioSource.PlayOneShot(open);
        while (timer < animTime)
        {
            transform.position = Vector3.Lerp(initialPos, targetPos, timer / animTime);
            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = targetPos;
        timer = 0;
        while (timer < animDelay)
        {

            yield return null;
            timer += Time.deltaTime;
        }

        StartCoroutine(CloseDoor());
    }


    private void OnEnable()
    {   
        transform.position = initialPosition;
        if (isOpen)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }
}
