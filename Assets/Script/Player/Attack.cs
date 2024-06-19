using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 5f;
    public float attackCooldown = 3f;
    private Animator anim;
    private bool currentlyAttacking = false;

    List<Enemy> targets = new List<Enemy>();
    bool readyToAttack = true;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (readyToAttack)  // Check if there are enemies nearby
            {
                AttackEnemies();

            }

        }
        if (readyToAttack)
            currentlyAttacking = false;
        else
            currentlyAttacking = true;

        if ((targets.Count <= 0) && (!currentlyAttacking))
            anim.ResetTrigger("isAttacking");

        damage = GetComponentInParent<PlayerStats>().damage;
    }

    private void AttackEnemies()
    {
        anim.SetTrigger("isAttacking");  // Trigger the attack animation
        foreach (Enemy e in targets)
        {

            if ((e.currentHealth - damage) <= 0)
            {
                e.Damage(damage);
                targets.Remove(e);

            }
            else
            {
                e.Damage(damage);

            }

        }
        readyToAttack = false;

        Invoke("ResetAttack", attackCooldown);
    }


    private void ResetAttack()
    {
        readyToAttack = true;
        anim.ResetTrigger("isAttacking");  // Reset the attack trigger
    }

}

