using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject InteractPopup;
    public GameObject player;
    public GameObject ghost;
    public GameObject DoorExit;

    public PlayerManager playerManager;

    private bool incollision = false; // because unity is useless when it comes to on trigger stay
    private string playertag; // just so it knows yippeee
    public bool IsHumanNeeded = true; // make ghosts great again (they SUCK!!!)

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (incollision)
            {
                if (playertag == "Player" && IsHumanNeeded == true)
                {
                    player.transform.position = DoorExit.transform.position;
                    InteractPopup.SetActive(true);
                }
                else if (IsHumanNeeded == false)
                {
                    if(playerManager.GhostRoom == true)
                    {
                        playerManager.GhostRoom = false;
                    }
                    else
                    {
                        playerManager.GhostRoom = true;
                    }
                    ghost.transform.position = DoorExit.transform.position;
                    InteractPopup.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && IsHumanNeeded == true)
        {
            InteractPopup.SetActive(true);
            incollision = true;
            playertag = collision.gameObject.tag;
        }
        else if(IsHumanNeeded == false)
        {
            InteractPopup.SetActive(true);
            incollision = true;
            playertag = collision.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Apparition"))
        {
            InteractPopup.SetActive(false);
            incollision = false;
        }
    }
}
