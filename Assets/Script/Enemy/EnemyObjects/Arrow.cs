using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 10;  // Damage the arrow will apply
    public Collider2D myCollider;  // Reference to the arrow's own collider

    void Start()
    {
        // Initialize the collider reference
        myCollider = GetComponent<Collider2D>();
        if (myCollider == null)
        {
            Debug.LogError("No Collider2D component found on the arrow!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
        if (playerStats != null && collision.collider == playerStats.targetCollider)
        {
            // Apply damage to the player
            playerStats.TakeDamage(damage);
            Debug.Log("Arrow applied " + damage + " damage to the player");

            // Destroy the arrow to prevent it from causing further damage
            Destroy(gameObject);
        }
      

        
    }
}
