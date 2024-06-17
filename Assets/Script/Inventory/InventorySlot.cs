using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon; // Reference to the icon image
    private Item currentItem; // Add a private field to hold the current item

    public void SetItem(Item item)
    {
        currentItem = item; // Store the current item
        icon.sprite = item.icon;
        icon.enabled = true; // Make the icon visible
    }

    public void ClearSlot()
    {
        currentItem = null; // Clear the current item reference
        icon.sprite = null;
        icon.enabled = false; // Hide the icon
    }

    // Implement the OnPointerClick method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem(); // Assuming DropItem handles item dropping logic
        }
    }

    private void DropItem()
    {
        if (currentItem != null && currentItem.itemPrefab != null)
        {
            Vector3 dropPosition = Player.instance.transform.position + new Vector3(1, 0, 0); // Adjust for 2D
            Instantiate(currentItem.itemPrefab, dropPosition, Quaternion.identity); // Drop the item near the player

            InventoryManager.instance.RemoveItem(currentItem.itemName); // Remove from inventory
            ClearSlot(); // Clear the UI slot
        }
    }



}
