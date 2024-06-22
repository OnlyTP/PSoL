using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bat : MonoBehaviour
{
    public float timeAbovePlayerTillDivebomb;
    public float flySpeed;
    public float divebombSpeed;
    public float chaseRadius;
    public float divebombRadius;
    public float heightAbovePlayerToHover;
    public float explosionRadius;
    public int damage;
    public GameObject explosionEffect;


    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;


    private float distanceToPlayer;
    private float distanceToHoverPosition;
    private Vector2 hoverPosition;
    private bool chasing;
    private bool diveBombing;
    private bool countDownToDivebomb;
    private float timeLeftTillDivebomb;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("Player does not have player tag or doesn't exist");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timeLeftTillDivebomb = timeAbovePlayerTillDivebomb;
    }

    private void Update()
    {
        if (countDownToDivebomb)
            timeLeftTillDivebomb -= Time.deltaTime;
        else
            timeLeftTillDivebomb = timeAbovePlayerTillDivebomb;

        float stepSize = flySpeed * Time.deltaTime;
        hoverPosition = new Vector2(player.transform.position.x, player.transform.position.y + heightAbovePlayerToHover);
        distanceToHoverPosition = Vector3.Distance(hoverPosition, transform.position);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        chasing = distanceToPlayer <= chaseRadius;
        countDownToDivebomb = distanceToHoverPosition <= divebombRadius && !diveBombing;
        diveBombing = timeLeftTillDivebomb <= 0;

        if (chasing && !diveBombing)
        {
            transform.position = Vector3.MoveTowards(transform.position, hoverPosition, stepSize);
            animator.SetBool("isAttacking", false);  // Keep flying
        }

        if (diveBombing)
        {
            Divebomb();
        }

        FlipSprite();
    }

    private void Divebomb()
    {
        animator.SetBool("isAttacking", true); // Begin attack animation
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        Vector2 forceDirection = transform.up * divebombSpeed;
        rb.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
            return;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        rb.velocity = Vector3.zero;
        if (distanceToPlayer <= explosionRadius)
            player.GetComponent<PlayerStats>().TakeDamage(damage);
        Destroy(gameObject);
    }

    private void FlipSprite()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Flip sprite to face left
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Normal orientation (facing right)
        }
    }
}