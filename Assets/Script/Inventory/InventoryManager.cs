using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventorySlotPrefab; // Assign in inspector
    public Transform itemsParent; // Assign in inspector

    private List<GameObject> slots = new List<GameObject>(); // Keeps track of instantiated slot GameObjects

    public List<Item> items = new List<Item>(); // The current items in the inventory

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        // Ensure that the inventory UI is updated to reflect the current inventory state when the game starts.
        UpdateInventoryUI();
    }

    // Call this method to add an item to the inventory
    public void AddItem(Item itemToAdd)
    {
        if (items.Count < 20) // assuming a max of 20 items for now
        {
            items.Add(itemToAdd);
            UpdateInventoryUI(); // Update the UI to reflect the added item
        }
    }

    // Call this method to remove an item from the inventory
    public void RemoveItem(string itemToRemove)
    {
        // First, find the item to remove
        Item item = items.FirstOrDefault(i => i.itemName == itemToRemove);

        if (item != null)
        {
            items.Remove(item); // Remove the item from the list
            UpdateInventoryUI(); // Update the UI to reflect the removed item
        }
    }



    public bool ContainsItem(string itemName)
    {
        foreach (Item i in items)
        {
            if (i.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }


    // Method to update the UI
    private void UpdateInventoryUI()
    {
        // Fetch all InventorySlot components in the itemsParent
        InventorySlot[] slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        // Loop through all slots and set the item or clear the slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                // If there is an item for this slot
                slots[i].SetItem(items[i]);
            }
            else
            {
                // If no item for this slot
                slots[i].ClearSlot();
            }
        }
    }
}
