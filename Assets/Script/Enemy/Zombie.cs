using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float timeBetweenHits;
    public float movementSpeed;
    public float chaseRadius;
    public float attackRadius;
    public int damage;

    private GameObject player;
    private Rigidbody2D rb;

    private bool chasing;
    private bool attacking;
    private bool readyToAttack = true;
    private float distanceToPlayer;
    private bool timeToChangeDirection = true;
    private Vector2 newPosition;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            Debug.Log("Player Doesn't Have Tag");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float stepSize = movementSpeed * Time.deltaTime;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        attacking = Vector3.Distance(transform.position, player.transform.position) <= attackRadius;
        chasing = Vector3.Distance(transform.position, player.transform.position) < chaseRadius;

        if (!chasing && timeToChangeDirection)
        {
            int rng = Random.Range(0, 2);
            if (rng == 0)
            {
                newPosition = new Vector2(transform.position.x + 5, transform.position.y);
            }
            else
            {
                newPosition = new Vector2(transform.position.x - 5, transform.position.y);
            }
            timeToChangeDirection = false;
            Invoke("ResetChangeDirection", 2);
        }

        if (!chasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, stepSize);
        }

        if (chasing && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stepSize);
        }

        if (attacking && readyToAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        player.GetComponent<PlayerStats>().TakeDamage(damage);
        readyToAttack = false;
        Invoke("ResetAttack", timeBetweenHits);
    }

    private void ResetAttack()
    {
        readyToAttack = true;
    }

    private void ResetChangeDirection()
    {
        timeToChangeDirection = true;
    }
}