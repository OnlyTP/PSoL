using UnityEngine;


public class PickupableItem : MonoBehaviour
{
    public Item item; // The item that this pickup represents

    private void OnMouseDown()
    {
        InventoryManager.instance.AddItem(item);
        gameObject.SetActive(false); // Optionally disable the item in the scene
    }
}
