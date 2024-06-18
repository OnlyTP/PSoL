using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    InventoryManager inventory;
    public GameObject craftingUI;


    private void Start()
    {
        inventory = InventoryManager.instance;
        craftingUI.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            craftingUI.SetActive(!craftingUI.activeInHierarchy);
        }
    }

    public void CraftRedPotion()
    {
        if (inventory.ContainsItem("NormalMushroom") && inventory.ContainsItem("NormalFlower"))
        {
            Debug.Log("Heals for 30");

            inventory.RemoveItem("NormalMushroom");
            inventory.RemoveItem("NormalFlower");

            Player.playerStats.Heal(30);
            return;
        }

        if (inventory.ContainsItem("DecayedMushroom") && inventory.ContainsItem("NormalFlower"))
        {
            Debug.Log("Heals for 20");

            inventory.RemoveItem("DecayedMushroom");
            inventory.RemoveItem("NormalFlower");

            Player.playerStats.Heal(20);
            return;
        }

        if (inventory.ContainsItem("NormalMushroom") && inventory.ContainsItem("DecayedFlower"))
        {
            Debug.Log("Heals for 20");

            inventory.RemoveItem("NormalMushroom");
            inventory.RemoveItem("DecayedFlower");

            Player.playerStats.Heal(20);
            return;
        }

        if (inventory.ContainsItem("DecayedMushroom") && inventory.ContainsItem("DecayedFlower"))
        {
            Debug.Log("Heals for 10");

            inventory.RemoveItem("DecayedMushroom");
            inventory.RemoveItem("DecayedFlower");

            Player.playerStats.Heal(10);
            return;
        }
    }

    public void CraftYellowPotion()
    {
        if (inventory.ContainsItem("NormalStrawberry") && inventory.ContainsItem("NormalFlower"))
        {
            Debug.Log("Damage increased by 5");

            inventory.RemoveItem("NormalStrawberry");
            inventory.RemoveItem("NormalFlower");

            Player.playerStats.IncreaseDamage(5);
            return;
        }

        if (inventory.ContainsItem("DecayedStrawberry") && inventory.ContainsItem("NormalFlower"))
        {
            Debug.Log("Damage increased by 3");

            inventory.RemoveItem("DecayedStrawberry");
            inventory.RemoveItem("NormalFlower");

            Player.playerStats.IncreaseDamage(3);
            return;
        }

        if (inventory.ContainsItem("NormalStrawberry") && inventory.ContainsItem("DecayedFlower"))
        {
            Debug.Log("Damage increased by 3");

            inventory.RemoveItem("NormalStrawberry");
            inventory.RemoveItem("DecayedFlower");

            Player.playerStats.IncreaseDamage(3);
            return;
        }

        if (inventory.ContainsItem("DecayedStrawberry") && inventory.ContainsItem("DecayedFlower"))
        {
            Debug.Log("Damage increased by 1");

            inventory.RemoveItem("DecayedStrawberry");
            inventory.RemoveItem("DecayedFlower");

            Player.playerStats.IncreaseDamage(1);
            return;
        }
    }

    public void CraftGreenPotion()
    {
        if (inventory.ContainsItem("NormalStrawberry") && inventory.ContainsItem("NormalMushroom"))
        {
            Debug.Log("Regen increased by 5");

            inventory.RemoveItem("NormalStrawberry");
            inventory.RemoveItem("NormalMushroom");

            Player.playerStats.IncreaseRegenRate(5);
            return;
        }

        if (inventory.ContainsItem("DecayedStrawberry") && inventory.ContainsItem("NormalMushroom"))
        {
            Debug.Log("Regen increased by 3");

            inventory.RemoveItem("DecayedStrawberry");
            inventory.RemoveItem("NormalMushroom");

            Player.playerStats.IncreaseRegenRate(3);
            return;
        }

        if (inventory.ContainsItem("NormalStrawberry") && inventory.ContainsItem("DecayedMushroom"))
        {
            Debug.Log("Regen increased by 3");

            inventory.RemoveItem("NormalStrawberry");
            inventory.RemoveItem("DecayedMushroom");

            Player.playerStats.IncreaseRegenRate(3);
            return;
        }

        if (inventory.ContainsItem("DecayedStrawberry") && inventory.ContainsItem("DecayedMushroom"))
        {
            Debug.Log("Regen increased by 1");

            inventory.RemoveItem("DecayedStrawberry");
            inventory.RemoveItem("DecayedMushroom");

            Player.playerStats.IncreaseRegenRate(1);
            return;
        }
    }
}
