using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int damage;
    public int regenRate = 0;
    public bool hasPotionEffect = false;  // Flag to check if any potion effect is active

    public Animator animator;
    public HealthBar healthBar;
    public PotionBar potionBar;
    public GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();

        if (regenRate > 0)
        {
            InvokeRepeating("RegenHealth", 3, 3);  // Every 3 seconds
        }

        if (CarryOver.levelHealth != 0)
        {
            currentHealth = CarryOver.levelHealth;

        }
    }


    private void Update()
    {
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        animator.SetTrigger("isDead");
        StartCoroutine(WaitForDeathAnimation());
    }

    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.685f);
        gameManager.GameOver();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void IncreaseDamage(int increaseAmount)
    {
        if (!hasPotionEffect)
        {
            damage += increaseAmount;
            hasPotionEffect = true;
            potionBar.StartPotionTimer(30);
            Invoke("ResetPotionEffect", 30);
        }
    }

    public void IncreaseRegenRate(int increaseAmount)
    {
        if (!hasPotionEffect)
        {
            regenRate += increaseAmount;
            hasPotionEffect = true;
            potionBar.StartPotionTimer(25);
            Invoke("ResetPotionEffect", 25);
        }
    }

    private void ResetPotionEffect()
    {
        damage = 5; // Reset to default or previous saved value
        regenRate = 0; // Reset to no regeneration or previous saved value
        hasPotionEffect = false;
    }

    private void RegenHealth()
    {
        if (regenRate > 0)
        {
            Heal(regenRate);
        }
    }

    private void OnDisable()
    {
        CarryOver.levelHealth = currentHealth;
    }
}
