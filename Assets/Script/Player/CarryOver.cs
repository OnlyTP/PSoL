using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOver
{
    public static int levelHealth;
    public static List<Item> items = new List<Item>();

    // Method to reset the carryover data for a new game session
    public static void ResetData()
    {
        levelHealth = 0;  // Reset health carryover
        items.Clear();    // Clear the list of items
        Debug.Log("CarryOver data has been reset.");
    }

    // Safely add items to the carryover list
    public static void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            Debug.Log("Item added to CarryOver: " + item.itemName);
        }
        else
        {
            Debug.LogError("Attempt to add a null item to CarryOver.");
        }
    }
}
