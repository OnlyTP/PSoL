using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool IsDead { get; private set; }

    private void Start()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            IsDead = true;
            Destroy(gameObject);
        }
    }
}
