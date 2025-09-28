using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{

    public GameObject InteractPopup;

    public Item item;

    private bool incollision = false; // because unity is useless when it comes to on trigger stay
    private string playertag; // just so it knows yippeee
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (incollision)
            {
                InventorySystem.instance.Add(item);
                InteractPopup.SetActive(false);
                Destroy(gameObject); // Adds item to the inventory system list then destroys it
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractPopup.SetActive(true);
        incollision = true;
        playertag = collision.gameObject.tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractPopup.SetActive(false);
        incollision = false;
    }
}
