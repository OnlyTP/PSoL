using UnityEngine;


public class PickupableItem : MonoBehaviour
{
    public Item item; // The item that this pickup represents


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject);
        }


    }
}
