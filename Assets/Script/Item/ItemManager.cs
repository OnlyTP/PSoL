using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items; // Array of all items in the game

    public void UpdateItemDecay(float currentTime)
    {
        foreach (var item in items)
        {
            item.UpdateDecay(currentTime);
        }
    }


}