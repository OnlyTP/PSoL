using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public float timeBetweenShots;
    public float movementSpeed;
    public float shootRadius;
    public GameObject arrow;
    public float arrowSpeed;

    private GameObject player;
    private Rigidbody2D rb;

    private bool shooting;
    private float distanceToPlayer;
    private bool canShoot = true;
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

        if (!shooting && timeToChangeDirection)
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

        if (!shooting)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, stepSize);
        }

        if (distanceToPlayer <= shootRadius)
        {
            shooting = true;
            Shoot();
        }
        else
        {
            shooting = false;
        }
    }

    private void Shoot()
    {
        if (!canShoot)
            return;

        GameObject _arrow = Instantiate(arrow, transform.position, Quaternion.identity);
        Rigidbody2D arrowRB = _arrow.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(player.transform.position.y - _arrow.transform.position.y, player.transform.position.x - _arrow.transform.position.x) * Mathf.Rad2Deg;
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        arrowRB.velocity = _arrow.transform.right * arrowSpeed;
        canShoot = false;
        Invoke("ResetShoot", timeBetweenShots);
    }

    private void ResetShoot()
    {
        canShoot = true;

    }

    private void ResetChangeDirection()
    {
        timeToChangeDirection = true;
    }

}