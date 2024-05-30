using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int maxFood = 10;
    public int currentFood;

    public HealthBar healthBar;
    public FoodBar foodBar;
    public GameManager gameManager;

    public Collider2D targetCollider;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentFood = maxFood;
        foodBar.SetMaxFood(maxFood);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(1);
        }    
        if (Input.GetKeyDown(KeyCode.B))
        {
            Hunger(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage taken: " + damage);
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            Die();
        }
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player has died.");
        gameManager.GameOver();
    }


    void Hunger (int hunger)
    {
        currentFood -= hunger;
        foodBar.SetFood(currentFood);
    }

}
