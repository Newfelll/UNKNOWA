using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject camera;
    private Camera cam;

    public List<LayerMask> colChannels = new List<LayerMask>();
    void Awake()
    {
        camera = transform.GetChild(0).gameObject;
        cam = camera.GetComponent<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           camera.SetActive(true);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            camera.SetActive(false);
        }
    }




  public void ChangeChannel(int channel)
    {
        cam.cullingMask = colChannels[channel];
        SFXSoundManager.Instance.PlaySwitchNoise();
    }
}
