using UnityEngine;


public class PickupableItem : MonoBehaviour
{
    public Item item; // The item that this pickup represents
    private void OnMouseDown()
    {
        if (canPickup)
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
    bool canPickup = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player")
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player")
        {
            canPickup = false;
        }
    }
}
