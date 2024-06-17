using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 5f;
    public float attackCooldown = 4f;

    List<Enemy> targets = new List<Enemy>();

    bool readyToAttack = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Remove(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (readyToAttack)
                AttackEnemies();
        }

        damage = GetComponentInParent<PlayerStats>().damage;
    }

    private void AttackEnemies()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Enemy e = targets[i];
            e.Damage(damage);
        }
        readyToAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    private void ResetAttack()
    {
        readyToAttack = true;
    }
}
