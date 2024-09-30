using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPosePuzzleManage : MonoBehaviour
{
    public bool isPuzzleActive = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SuperPosition.isPuzzleActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SuperPosition.isPuzzleActive = false;
        }
    }

}
