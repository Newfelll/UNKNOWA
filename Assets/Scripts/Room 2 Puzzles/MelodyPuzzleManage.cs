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


        //if (selectedObject == null)
        //{
        //    selectedObject = selected;
        //    Debug.Log(selectedObject.GetComponent<Renderer>().material.GetColor("_EmissionColor"));
        //    Color color = selectedObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");

        //    selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", (color) * emissionIntensityMultiplier);
        //}
        //else
        //{
        //    Color color = selectedObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        //    selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", (color / emissionIntensityMultiplier));

        //    selectedObject = selected;
        //    Color color2 = selectedObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");

        //    selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", (color2 * emissionIntensityMultiplier)); 



        //}
    }


    IEnumerator PressCoroutine()
    {


        isaAnimationPlaying = true;


        selectedRenderer = selectedObject.GetComponent<Renderer>();



        //  Color colorDef = selectedRenderer.material.GetColor("_EmissionColor");

        
        Color colorDef= selectedRenderer.sharedMaterial.GetColor("_EmissionColor");
        
        Color colorAnim = colorDef * emissionIntensityMultiplier;

        Vector3 initialPos = selectedObject.transform.localPosition;
        Vector3 animatedPos = new Vector3(selectedObject.transform.localPosition.x, 0.64f, selectedObject.transform.localPosition.z);


        while (timer < animationTime)
        {

            selectedObject.transform.localPosition = Vector3.Lerp(selectedObject.transform.localPosition, animatedPos, timer / animationTime);

            Mpb.SetColor("_EmissionColor", Color.Lerp(colorDef, colorAnim, timer / animationTime));
            selectedRenderer.SetPropertyBlock(Mpb);

          //  selectedRenderer.material.SetColor("_EmissionColor", Color.Lerp(colorDef, colorAnim, timer / animationTime));


            // emissionIntensityDef = Mathf.Lerp(emissionIntensityDef, emissionIntensityMultiplier, timer / animationTime);

            // selectedRenderer.material.SetColor("_EmissionColor", (color) * emissionIntensityDef);

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;


        while (timer < animationTime)
        {
            selectedObject.transform.localPosition = Vector3.Lerp(selectedObject.transform.localPosition, initialPos, timer / animationTime);

            Mpb.SetColor("_EmissionColor", Color.Lerp(colorAnim, colorDef, timer / animationTime));
            selectedRenderer.SetPropertyBlock(Mpb);

            //selectedRenderer.material.SetColor("_EmissionColor", Color.Lerp(colorAnim, colorDef, timer / animationTime));

            //  emissionIntensityDef = Mathf.Lerp(emissionIntensityDef, 0, timer / animationTime);
            // selectedRenderer.material.SetColor("_EmissionColor", (color) / emissionIntensityDef);

            timer += Time.deltaTime;
            yield return null;
        }

        Mpb.SetColor("_EmissionColor", colorDef);
        selectedRenderer.SetPropertyBlock(Mpb);
        selectedRenderer.sharedMaterial.SetColor("_EmissionColor", colorDef);
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
            Debug.Log("Melody Correct");
            SFXSoundManager.Instance.PlayCorrectSFX();

            Mpb.SetColor("_EmissionColor", Color.green * 3);
            CorrectnessIndýcator.GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);

            onMelodySolved.TriggerEvent();
        }
        else
        {
            Debug.Log("Melody Incorrect");
        }



        /* if (isMelodySolved) return;
        if (melody.Count == 5)
        {
            
            melody.RemoveAt(0);

            melody.Add(note);


            if (correctMelody.SequenceEqual(melody))
            {   isMelodySolved = true;
                Debug.Log("Melody Correct");
                SFXSoundManager.Instance.PlayCorrectSFX();

                Mpb.SetColor("_EmissionColor", Color.green*3);
                CorrectnessIndýcator.GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);
                //CorrectnessIndýcator.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                
                onMelodySolved.TriggerEvent();
                

            }
            else Debug.Log("Melody InCorrect");
        }
        else
        {
            melody.Add(note);

            

            if (correctMelody.SequenceEqual(melody))
            {
                isMelodySolved = true;
                Debug.Log("Melody Correct");
                SFXSoundManager.Instance.PlayCorrectSFX();
                Mpb.SetColor("_EmissionColor", Color.green*3);
                CorrectnessIndýcator.GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);
               // CorrectnessIndýcator.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                onMelodySolved.TriggerEvent();

            }
            else Debug.Log("Melody InCorrect");
        }*/
    }
}
