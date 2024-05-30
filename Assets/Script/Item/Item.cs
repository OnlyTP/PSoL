using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon;
    public GameObject itemPrefab; // The prefab to instantiate when this item is used or dropped
    public Sprite normalSprite; // Sprite before decay
    public Sprite decayedSprite; // Sprite after decay
    public float timeUntilDecay = 000.3f; // Time in seconds until the item decays
    public int normalQuality = 100; // Quality when normal
    public int decayedQuality = 50; // Quality when decayed
    public int quality; // Current quality of the item

    // Runtime properties
    [HideInInspector]
    public bool hasDecayed = false;
    private float decayTimer;

    // Initialize or reset the item
    public void Initialize()
    {
        if (itemPrefab != null)
        {
            var spriteRenderer = itemPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = normalSprite;
            }
        }
        decayTimer = timeUntilDecay;
        hasDecayed = false;
        UpdateQuality(normalQuality);
    }

    // Call this method regularly to update decay based on elapsed time
    public void UpdateDecay(float deltaTime)
    {
        if (!hasDecayed && decayTimer > 0)
        {
            decayTimer -= deltaTime;
            if (decayTimer <= 0)
            {
                DecayItem();
            }
        }
    }

    // Handles the decay transformation of the item
    private void DecayItem()
    {
        if (itemPrefab != null)
        {
            var spriteRenderer = itemPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = decayedSprite; // Change to decayed sprite
            }
        }
        hasDecayed = true;
        UpdateQuality(decayedQuality);
    }

    // Update the quality of the item
    private void UpdateQuality(int newQuality)
    {
        quality = newQuality;
     
    }

}
