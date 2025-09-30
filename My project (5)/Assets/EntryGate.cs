using UnityEngine;

public class EntryGate : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject activateQ;
    public InventorySystem inventory;
    public Item key;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void onCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           activateQ.SetActive(true); 
        }
    }

    void onCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           activateQ.SetActive(false); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inventory.items.Contains(key))
        {
            Destroy(gameObject);
        }
    }
}
