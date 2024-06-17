using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon;
    public GameObject itemPrefab; // The prefab to instantiate when this item is used or dropped
    public int quality; // Current quality of the item

    // Initialize or reset the item
    public void Initialize()
    {
        if (itemPrefab != null)
        {
            var spriteRenderer = itemPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = icon; // Assuming the 'icon' as default sprite
            }
        }
        quality = 100; // Assuming some default quality value
    }

    // Update the quality of the item
    private void UpdateQuality(int newQuality)
    {
        quality = newQuality;
    }
}
