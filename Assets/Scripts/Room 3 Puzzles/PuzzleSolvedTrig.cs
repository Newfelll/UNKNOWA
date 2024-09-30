using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolvedTrig : MonoBehaviour
{
    public List<GameObject> puzzlePieces;
    
   
  


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            GetComponentInParent<WeightSenseManager>().OpenDoor();

            foreach(GameObject pieces in puzzlePieces)
            {
                pieces.transform.localScale = pieces.GetComponent<Visible>().puzzleSolvedScale;
                pieces.GetComponent<Visible>().enabled = false;
            }
        }
    }
}
