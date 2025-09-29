using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject InteractPopup;
    public GameObject player;
    public GameObject DoorExit;

    private bool incollision = false; // because unity is useless when it comes to on trigger stay
    private string playertag; // just so it knows yippeee
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (incollision)
            {
                if (playertag == "Player")
                {
                    player.transform.position = DoorExit.transform.position;
                    InteractPopup.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InteractPopup.SetActive(true);
            incollision = true;
            playertag = collision.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractPopup.SetActive(false);
            incollision = false;
        }
    }
}
