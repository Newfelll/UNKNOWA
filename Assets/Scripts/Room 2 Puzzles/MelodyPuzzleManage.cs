using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MelodyPuzzleManage : MonoBehaviour
{


    public GameObject selectedObject;
    public GameObject CorrectnessIndýcator;
    private Renderer selectedRenderer;
    private AudioSource audioSource;
    

    [SerializeField] private List<int> melody = new List<int>(5);
    [SerializeField] private List<int> correctMelody = new List<int>(5) { 1, 2, 3, 4,5 };
    [SerializeField] private List<AudioClip> notes = new List<AudioClip>(5);

    [SerializeField] private float animationTime = 0.5f;
    private float timer = 0f;
    private bool isaAnimationPlaying = false;

    [SerializeField] private float emissionIntensityDef = 0;
    [SerializeField] private float emissionIntensityMultiplier = 4;

    private bool isMelodySolved = false;
    public GameEvent onMelodySolved;




    MaterialPropertyBlock mpb;
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

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PressNote(GameObject selected)
    {
        if (!isaAnimationPlaying)
        {
            selectedObject = selected;
            char ch = selectedObject.name[0];
            int intValue = ch - '0';
            
            AddNoteAndCheckMelody(intValue);

            audioSource.PlayOneShot(notes[intValue-1]);

            StartCoroutine(PressCoroutine());
        }


    }


   

    IEnumerator PressCoroutine()
    {


        isaAnimationPlaying = true;


        selectedRenderer = selectedObject.GetComponent<Renderer>();



        Vector3 initialPos = selectedObject.transform.localPosition;
        Vector3 animatedPos = new Vector3(selectedObject.transform.localPosition.x, 0.64f, selectedObject.transform.localPosition.z);

        Mpb.SetColor("_EmissionColor", selectedRenderer.sharedMaterial.GetColor("_Color") * 3);
        selectedRenderer.SetPropertyBlock(Mpb);

     
        

        while (timer < animationTime)
        {

            selectedObject.transform.localPosition = Vector3.Lerp(selectedObject.transform.localPosition, animatedPos, timer / animationTime);


          

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;


        while (timer < animationTime)
        {
            selectedObject.transform.localPosition = Vector3.Lerp(selectedObject.transform.localPosition, initialPos, timer / animationTime);

          


            timer += Time.deltaTime;
            yield return null;
        }

        Mpb.SetColor("_EmissionColor", selectedRenderer.sharedMaterial.GetColor("_Color"));
        selectedRenderer.SetPropertyBlock(Mpb);


    
        emissionIntensityDef = 0;
        selectedObject.transform.localPosition = initialPos;
        timer = 0;
        isaAnimationPlaying = false;


    }


    void AddNoteAndCheckMelody(int note)
    {
        if (isMelodySolved) return;

        if (melody.Count == 5)
        {
            melody.RemoveAt(0);
        }

        melody.Add(note);

        if (correctMelody.SequenceEqual(melody))
        {
            isMelodySolved = true;
            
            SFXSoundManager.Instance.PlayCorrectSFX();
            
            Mpb.SetColor("_EmissionColor", Color.green * 3);
            CorrectnessIndýcator.GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);

            onMelodySolved.TriggerEvent();
        }
        else
        {
            
        }



        
    }
}
