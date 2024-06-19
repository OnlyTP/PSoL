using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public float timeBetweenShots;
    public float shootRadius;
    public GameObject arrow;
    public float arrowSpeed;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool shooting;
    private float distanceToPlayer;
    private bool canShoot = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("Player Doesn't Have Tag");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check shooting range
        if (distanceToPlayer <= shootRadius)
        {
            if (canShoot)
            {
                shooting = true;
                Shoot();
                animator.SetBool("isAttacking", true);
                animator.SetBool("isIdle", false);
            }
        }
        else
        {
            shooting = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", true);
        }

        // Flip sprite based on player position
        FlipSprite();
    }

    private void Shoot()
    {
        if (!canShoot)
            return;

        GameObject _arrow = Instantiate(arrow, transform.position, Quaternion.identity);
        Rigidbody2D arrowRB = _arrow.GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(player.transform.position.y - _arrow.transform.position.y,
                                  player.transform.position.x - _arrow.transform.position.x) * Mathf.Rad2Deg;
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        arrowRB.velocity = _arrow.transform.right * arrowSpeed;

        canShoot = false;
        Invoke("ResetShoot", timeBetweenShots);
    }

    private void ResetShoot()
    {
        canShoot = true;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdle", true);
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
