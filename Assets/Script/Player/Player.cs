using UnityEngine;

public class Player : MonoBehaviour
{
    // Static instance of the Player class
    public static Player instance;

    public static PlayerStats playerStats;
    public static PlayerController playerController;


    private void Awake()
    {
        // Check if instance already exists
        if (instance != null && instance != this)
        {
            // If so, destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
        else
        {
            // This GameObject is the instance
            instance = this;
          
        }

        playerStats = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
    }
}
