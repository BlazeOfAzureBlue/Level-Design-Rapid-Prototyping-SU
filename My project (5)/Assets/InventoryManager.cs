using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class InventorySystem : MonoBehaviour
{


    public event EventHandler<Item> itemSelected;

    float inventoryDebounce = 0;
    bool inventoryActive = false;
    public GameObject inventory; // Reference to the inventory GUI in gamespace
    public GameObject canvas;
    public GameObject itemDescriptionHolderActual;
    public TMP_Text itemDescHolder;
    public Image itemImageHolder;


    public static InventorySystem instance; // Creates new instance of InventorySystem / main instance
    public List<Item> items = new List<Item>();  // Creates a list to store all the items
    public Dictionary<Item, Transform> ItemObjectPairs = new Dictionary<Item, Transform>();

    public Transform ItemContent; // Reference to the content gui in game space
    public Transform InventoryItem; // Reference to the asset of "Item" to be placed in the GUI

    public Camera mainCamera;

    private bool itemContained;

    public void Awake()
    {
        instance = this; 
    }

    public void Update()
    {

        inventoryDebounce -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.I) && inventoryDebounce <= 0) // If I is pressed, checks if the player already has the inventory open. If not, opens it. 
        {
            if (inventoryActive == true)
            {
                inventoryDebounce = 0.5f;
                canvas.SetActive(false);
                inventoryActive = false;

                ClearItems(); // Clears the items from the inventory GUI to prevent replication
            }
            else
            {
                if (inventoryActive == false)
                {

                    inventoryDebounce = 0.5f;
                    canvas.SetActive(true);
                    inventoryActive = true;
                    itemImageHolder.enabled = false;
                    itemDescriptionHolderActual.SetActive(false);
                    ListItems(); // Adds all items to the inventory GUI

                }

            }
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ClearItems()
    {
        foreach (Transform item in ItemContent) // Goes through content GUI and deletes all items
        {
            Destroy(item.gameObject);
        }
    }

    public bool ContainItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ListItems()
    {
        foreach (var item in items)
        {

            Transform obj = Instantiate(InventoryItem.transform, ItemContent); // Goes through each item in the list, creating it as an item in the GUI using the previously made GUI model
            var ItemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>(); // Retrieves item name from object
            ItemName.text = item.itemName; // Assigns the values to the item GUI object 

            ItemObjectPairs[item] = obj;
            obj.GetComponent<Button>().onClick.AddListener(() => { SelectItem(item); });

        }
    }
    public void SelectItem(Item itemPassed)
    {
        itemDescHolder.text = itemPassed.itemDescription;
        itemImageHolder.sprite = itemPassed.imageSprite;
        itemDescriptionHolderActual.SetActive(true);
        itemImageHolder.enabled = true;
        itemSelected?.Invoke(this, itemPassed);
    }
}
