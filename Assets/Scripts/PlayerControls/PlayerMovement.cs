using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameEvents")]
    public GameEvent onDeath;
    public GameEvent onRespawn;
    public GameEvent onKeyPick;


    Scene scene;

    [Header("References")]
    [SerializeField] Transform orientation;
    private Rigidbody rb;
    public Camera playerCam;
    public AudioSource audioSourceFootsteps;
    public AudioSource audioSourceMixed;
    public AudioClip pickupSound;
    public AudioClip jumpPadSound;
    public AudioClip landingSound;
    public Animator transition;




    [Header("Player Movement")]

    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundMoveSpeed = 6f;
    [SerializeField] private float moveMultiplier = 10f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float airMoveMultiplier = 0.4f;
    [SerializeField] private bool isWalking = true;
    [SerializeField] private float footStepInterval=0.5f;
    [SerializeField] private float landingSoundThreshold=5;
    private Vector3 velocityBeforePhysicsUpdate;








    [Header("Jump")]
    [SerializeField] private float canDoubleJump = 1;
    [SerializeField] private bool doubleJumpToggle;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool jump = false;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpPadMultiplier = 10f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;

    public LayerMask groundMask;










    [Header("Vectors")]
    public Vector3 moveDir;
    Vector3 slopeMoveDir;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;






    private float horizontalMovement;
    private float verticalMovement;




    private Vector3 respawnPosition;


    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else { return false; }
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {   
        respawnPosition= Vector3.zero;

        scene = SceneManager.GetActiveScene();

       

        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        moveSpeed = groundMoveSpeed;
       


    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance.isSceneActive)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {



                StartCoroutine(Respawn());

            }



            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);






            if (isGrounded)
            {

                canDoubleJump = 1;
            }


            if (isGrounded && Input.GetKeyDown(jumpKey))
            {
                jump = true;


            }
            else if (Input.GetKeyDown(jumpKey) && !isGrounded && canDoubleJump == 1 && doubleJumpToggle)
            {
                jump = true;
                canDoubleJump = 0;
            }


            slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);



            PlayerInput();

            ControlDrag();

            if (isGrounded && isWalking && (horizontalMovement != 0 || verticalMovement != 0))
            {
                isWalking = false;
                StartCoroutine(PlayFootsteps());

                audioSourceFootsteps.pitch = Random.Range(1f, 1.5f);
                audioSourceFootsteps.loop = true;
            }


        }
        
    }
    void FixedUpdate()
    { if (GameManager.Instance.isSceneActive)
        {


            MovePlayer();

            if (jump)
            {
                jump = false;
                Jump();
            }

            velocityBeforePhysicsUpdate = rb.velocity;
        }
    }

    

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDir = orientation.transform.forward * verticalMovement + orientation.transform.right * horizontalMovement;


    }

    void ControlDrag()
    {

        if (isGrounded)
        {
            rb.drag = groundDrag;
            moveSpeed = groundMoveSpeed;

        }
        else
        {
            rb.drag = airDrag;
            moveSpeed = groundMoveSpeed;
        }

    }

    void MovePlayer()
    {
        if (isGrounded & OnSlope())
        {
            rb.AddForce(slopeMoveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            

        }
        else if (isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            

        }
        else
        {
            
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier * airMoveMultiplier, ForceMode.Acceleration);

        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }


    }









    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jumppad")
        {   
            audioSourceMixed.PlayOneShot(jumpPadSound);
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpPadMultiplier, ForceMode.Impulse);

        }
        
        if ( velocityBeforePhysicsUpdate.y<landingSoundThreshold && collision.gameObject.tag == "Ground")
        {   
            audioSourceMixed.PlayOneShot(landingSound);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
    }


    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Death")
        {
            onDeath.TriggerEvent();
            
        }

        if (other.gameObject.tag == "Key")
        {   
           Destroy(other.gameObject);
           onKeyPick.TriggerEvent();
           audioSourceMixed.PlayOneShot(pickupSound);  
            
            
        }

        if (other.gameObject.tag == "Finish")
        {
            GameManager.Instance.isSceneActive = false;
            rb.velocity = Vector3.zero;
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            FindObjectOfType<LevelLoader>().LoadLevel(sceneIndex+1);
            

        }

    }



    public void OnDeath()
    {
       StartCoroutine(Respawn());
         
        
        onRespawn.TriggerEvent();
    }


  

    IEnumerator PlayFootsteps()
    {   
        audioSourceFootsteps.pitch = Random.Range(1f, 1.5f);
        audioSourceFootsteps.PlayOneShot(audioSourceFootsteps.clip);
        yield return new WaitForSeconds(footStepInterval);
        isWalking = true;
    }


    IEnumerator Respawn()
    {

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        transition.SetTrigger("End");
        rb.velocity = Vector3.zero;
        rb.position = transform.parent.position;
        onRespawn.TriggerEvent();


    }

}