using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class PlayerManager : MonoBehaviour
{

    [Header("Actual attributes to change of player")]
    public float speed = 5f;
    public LayerMask groundLayer;
    public float groundCheckRadius = 1.5f;

    public bool LevitationAcquired = false;
    public bool AscensionAcquired = false;

    [Header("put the different stupid stuff in here so the code knows what you're talking to")]
    public GameObject apparition;
    public GameObject mainCharacter;
    public Rigidbody2D mainRigidBody;
    public Rigidbody2D apparitionRigidBody; // get both rigid bodies as it makes life easier for me

    public GameObject mainCamera;
    public GameObject apparitionCamera; // have a separate camera for both of them 

    public Transform groundCheck;
    public Transform ApparitiongroundCheck;

    public bool GhostRoom;

    public Image panel;

    // private stuff mwhahahaha
    private bool isGrounded;
    private float playerMovement;
    private bool appartionActive;
    private bool abletoLevitate = false;

    private float LevitationTime = 5.0f;
    private float AscendTime = 3.0f;
    private bool isLevitating = false;
    private bool isAscending = false;



    private IEnumerator Levitation()
    {
        yield return new WaitForSeconds(0.5f);
        abletoLevitate = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void ResetApparition()
    {
        apparition.SetActive(false);
        apparitionCamera.SetActive(false);

        mainCamera.SetActive(true);
        appartionActive = false;
        panel.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (appartionActive)
        {
            isGrounded = Physics2D.OverlapCircle(ApparitiongroundCheck.position, groundCheckRadius, groundLayer); /// ooooo is the ghost grounded ooooooo
            if (isGrounded)
            {
                abletoLevitate = false;
                LevitationTime = 5.0f;
                AscendTime = 3.0f;
            }
            if (Input.GetButton("Jump") && !isGrounded && abletoLevitate && LevitationTime > 0 && isAscending == false && LevitationAcquired == true)
            {
                apparitionRigidBody.linearVelocity = new Vector2(apparitionRigidBody.linearVelocityX, 0f);
                LevitationTime = LevitationTime - Time.deltaTime;
                isLevitating = true;
            } // LEVITATE! LEVITATE! LEVITATE!

            if (Input.GetButton("Ascend") && isLevitating == false && AscendTime > 0 && AscensionAcquired == true)
            {
                apparitionRigidBody.linearVelocity = new Vector2(apparitionRigidBody.linearVelocityX, 7.5f);
                AscendTime = AscendTime - Time.deltaTime;
                isAscending = true;
            } // ASCEND! ASCEND! ASCEND!

            if (!Input.GetButton("Jump"))
            {
                isLevitating = false;
            }
            if (!Input.GetButton("Ascend"))
            {
                isAscending = false;
            } // player stopped doing anything cool :( BOOOOOOOOOOOOOO
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); /// ooooo is the player grounded ooooooo
        }

        if (Input.GetButtonDown("ActivateApparition") && isGrounded)
        {
            if(appartionActive)
            {
                ResetApparition();
            }
            else
            {
                // Note for future self: don't be stupid, be a smartie, use fucking layers for the apparition collision
                apparition.SetActive(true);
               
                apparition.transform.position = mainCharacter.transform.position;
                mainRigidBody.linearVelocity = new Vector2(0, 0);
                apparitionCamera.SetActive(true);

                mainCamera.SetActive(false);
                appartionActive = true;
            }
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            if(appartionActive)
            {
                apparitionRigidBody.linearVelocity = new Vector2(apparitionRigidBody.linearVelocityX, 9f);
                StartCoroutine(Levitation());
            }
            else
            {
                mainRigidBody.linearVelocity = new Vector2(mainRigidBody.linearVelocityX, 9f);
            }
        }

           

        if (appartionActive && Vector2.Distance(apparition.transform.position, mainCharacter.transform.position) > 0 && GhostRoom == false)
        {
            float distance = 50 - Vector2.Distance(apparition.transform.position, mainCharacter.transform.position);
            float alpha = 1f - Mathf.Clamp01(distance / 50); // calculate the alpha teehee
            print(distance);

            panel.color = new Color(0, 0, 0, alpha);

            if (distance <= 0)
            {
                appartionActive = false;
                apparition.SetActive(false);
                apparitionCamera.SetActive(false);

                mainCamera.SetActive(true);
                panel.color = new Color(0, 0, 0, 0);
            } // ruh roh - you wandered too far!!! now you get put in the *f o r e v e r b o x*
        }
        playerMovement = Input.GetAxis("Horizontal");
    }


    private void FixedUpdate()
    {
        if(appartionActive)
        {
           apparitionRigidBody.linearVelocity = new Vector2(playerMovement * speed, apparitionRigidBody.linearVelocityY);
        }
        else
        {
            mainRigidBody.linearVelocity = new Vector2(playerMovement * speed, mainRigidBody.linearVelocityY);
        }
    }
}


