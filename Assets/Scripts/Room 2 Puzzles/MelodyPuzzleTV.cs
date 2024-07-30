using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPuzzleTV : MonoBehaviour
{
    
    public GameObject StaticMelody_1;
    public GameObject StaticMelody_2;

    [SerializeField] private float switchTime = 0.5f;




    void Start()
    {
        InvokeRepeating("ChangeStatic", switchTime, switchTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeStatic()
    {
        StaticMelody_1.SetActive(!StaticMelody_1.active);
        StaticMelody_2.SetActive(!StaticMelody_2.active);
    }
}
