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

    private int originalDamage;
    private int originalRegenRate;

    public Animator animator;
    public HealthBar healthBar;
    public PotionBar potionBar;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);

        if (CarryOver.levelHealth > 0)
        {
            currentHealth = CarryOver.levelHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }
     
        healthBar.SetHealth(currentHealth);

        if (regenRate > 0)
        {
            InvokeRepeating("RegenHealth", 3, 3);  // Every 3 seconds
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
        if (!animator.GetBool("isDead"))
        {
            animator.SetTrigger("isDead");
            StartCoroutine(WaitForDeathAnimation());
            // Directly disable player controls here if not using GameManager
            this.GetComponent<PlayerController>().enabled = false;
        }
    }



    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.685f);
        gameManager.GameOver();
        ResetProgress();
    }

    void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);  // Resets the unlock level to 1
        PlayerPrefs.Save();
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
            originalDamage = damage;
            damage += increaseAmount;
            ActivatePotionEffect(30);
        }
    }

    public void IncreaseRegenRate(int increaseAmount)
    {
        if (!hasPotionEffect)
        {
            originalRegenRate = regenRate;
            regenRate += increaseAmount;
            ActivatePotionEffect(25);
        }
    }

    private void ResetPotionEffect()
    {
        damage = originalDamage;
        regenRate = originalRegenRate;
        hasPotionEffect = false;
    }


    private void RegenHealth()
    {
        if (regenRate > 0)
        {
            Heal(regenRate);
        }
    }

    private void ActivatePotionEffect(int duration)
    {
        hasPotionEffect = true;
        potionBar.StartPotionTimer(duration);
        Invoke("ResetPotionEffect", duration);
    }

    private void OnDisable()
    {
        CarryOver.levelHealth = currentHealth;
    }
}
