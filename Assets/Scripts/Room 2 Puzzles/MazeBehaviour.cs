using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeBehaviour : MonoBehaviour
{
    public List<Color> lightColor=new List<Color>(13);
    public Light light;

    [SerializeField] private bool isPlayerInside = false;
    [SerializeField] private int delay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }



   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            StartCoroutine(PlayColorSequnce());
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            light.color = Color.black;
            
        }
    }

    IEnumerator Wait()
    {   
        
        yield return new WaitForSeconds(delay);
    }


    IEnumerator PlayColorSequnce()
    {  
        
        foreach (var color in lightColor)
        {
            if (isPlayerInside)
            {


                light.color = color;
                yield return new WaitForSeconds(delay);
            }
            else break;
        }

    }
}
