using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    private int leverPressed = 0;
    public List <GameObject> CorrectnessIndýcators= new List<GameObject>(4);
    public GameObject prize;
    MaterialPropertyBlock mpb;
    Material mat;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

   

    // Update is called once per frame
  

    public void LeverPressed()
    {
       
        Mpb.SetColor("_EmissionColor", Color.green * 3);
        CorrectnessIndýcators[leverPressed].GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);

        leverPressed++;
        if (leverPressed == 4)
        {
            SFXSoundManager.Instance.PlayCorrectSFX();
            prize.SetActive(true);
        }
    }

  
}
