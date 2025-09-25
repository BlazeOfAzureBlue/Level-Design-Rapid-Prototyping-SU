using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public TextInteractionManager TextInteractionManager;
    public GameObject InteractPopup;

    public string HumanString = "Input text here.";
    public string ApparitionString = "Input text here.";

    private bool incollision = false; // because unity is useless when it comes to on trigger stay
    private string playertag; // just so it knows yippeee
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(incollision)
            {
                if (playertag == "Player")
                {
                    TextInteractionManager.ActivateTypewriter(HumanString);
                    InteractPopup.SetActive(false);
                }
                else
                {
                    TextInteractionManager.ActivateTypewriter(ApparitionString);
                    InteractPopup.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractPopup.SetActive(true);
        incollision = true;
        playertag = collision.gameObject.tag;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("1");
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("ok");
            if (collision.gameObject.CompareTag("Player"))
            {
                TextInteractionManager.ActivateTypewriter(HumanString);
                InteractPopup.SetActive(false);
            }
            else
            {
                TextInteractionManager.ActivateTypewriter(ApparitionString);
                InteractPopup.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractPopup.SetActive(false);
        incollision = false;
    }
}
