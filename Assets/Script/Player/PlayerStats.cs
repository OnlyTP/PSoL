using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int damage;
    public int regenRate = 0;
    public bool hasPotionEffect = false;

    public Animator animator;


    public HealthBar healthBar;
    public PotionBar potionBar;
    public GameManager gameManager;

    public Collider2D targetCollider;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();

        RegenHealth();
    }

    // Update is called once per frame
    void Update()
    {
      
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
        animator.SetTrigger("isDead");  // Trigger the death animation
        StartCoroutine(WaitForDeathAnimation());
    }

    IEnumerator WaitForDeathAnimation()
    {
        // Wait for the length of the death animation
        yield return new WaitForSeconds(0.685f);  // Animation clip length is 0.683 seconds
        gameManager.GameOver();  // Call GameOver method after the animation completes
    }


    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

   
    
    public void IncreaseDamage(int increaseAmount)
    {
        if (!hasPotionEffect)
        {
            damage += increaseAmount;
            hasPotionEffect = true;
            potionBar.StartPotionTimer(30);
            Invoke("ResetDamage", 30);
        }
    }

    private void ResetDamage()
    {
        damage = 5;
        hasPotionEffect = false;
    }

    public void IncreaseRegenRate(int increaseAmount)
    {
        if (!hasPotionEffect)
        {
            regenRate += increaseAmount;
            hasPotionEffect = true;
            potionBar.StartPotionTimer(25);
            Invoke("ResetRegenRate", 25);
        }
    }

    private void ResetRegenRate()
    {
        regenRate = 0;
        hasPotionEffect = false;
    }

    private void RegenHealth()
    {
        Heal(regenRate);
        Invoke("RegenHealth", 3);
    }

}
