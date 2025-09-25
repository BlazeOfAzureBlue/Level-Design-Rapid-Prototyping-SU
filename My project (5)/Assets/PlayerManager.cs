using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerManager : MonoBehaviour
{

    [Header("Actual attributes to change of player")]
    public float speed = 5f;
    public LayerMask groundLayer;
    public float groundCheckRadius = 1.5f;

    [Header("put the different stupid stuff in here so the code knows what you're talking to")]
    public GameObject apparition;
    public GameObject mainCharacter;
    public Rigidbody2D mainRigidBody;
    public Rigidbody2D apparitionRigidBody; // get both rigid bodies as it makes life easier for me

    public GameObject mainCamera;
    public GameObject apparitionCamera; // have a separate camera for both of them 

    public Transform groundCheck;
    public Transform ApparitiongroundCheck; 

    public Image panel;

    // private stuff mwhahahaha
    private bool isGrounded;
    private float playerMovement;
    private bool appartionActive;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(appartionActive)
        {
            isGrounded = Physics2D.OverlapCircle(ApparitiongroundCheck.position, groundCheckRadius, groundLayer); /// ooooo is the ghost grounded ooooooo
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); /// ooooo is the player grounded ooooooo
        }

        if (Input.GetButtonDown("ActivateApparition") && isGrounded)
        {
            if(appartionActive)
            {
                apparition.SetActive(false);
                apparitionCamera.SetActive(false);

                mainCamera.SetActive(true);
                appartionActive = false;
                panel.color = new Color(0, 0, 0, 0);
            }
            else
            {
                // Note for future self: don't be stupid, be a smartie, use fucking layers for the apparition collision
                apparition.SetActive(true);
               
                apparition.transform.position = mainCharacter.transform.position;
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
            }
            else
            {
                mainRigidBody.linearVelocity = new Vector2(mainRigidBody.linearVelocityX, 9f);
            }
        }

        if(appartionActive && Vector2.Distance(apparition.transform.position, mainCharacter.transform.position) > 0)
        {
            float distance = 50 - Vector2.Distance(apparition.transform.position, mainCharacter.transform.position);
            float alpha = 1f - Mathf.Clamp01(distance / 50); // calculate the alpha teehee
            print(distance);

            panel.color = new Color(0, 0, 0, alpha);

            if(distance <= 0)
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
