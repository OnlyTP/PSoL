using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public float timeBetweenShots;
    public float movementSpeed;
    public float chaseRadius;
    public float shootRadius;
    public GameObject arrow;
    public float arrowSpeed;

    private GameObject player;
    private Rigidbody2D rb;

    private float distanceToPlayer;
    private bool canShoot = true;


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


        if (distanceToPlayer <= shootRadius && canShoot)
        {
            Shoot();
            Invoke("ResetShoot", timeBetweenShots);
        }
    }

    private void Shoot()
    {
        GameObject _arrow = Instantiate(arrow, transform.position, Quaternion.identity);
        Rigidbody2D arrowRB = _arrow.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(player.transform.position.y - _arrow.transform.position.y, player.transform.position.x - _arrow.transform.position.x) * Mathf.Rad2Deg;
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        arrowRB.velocity = _arrow.transform.right * arrowSpeed;
        canShoot = false;
    }

    private void ResetShoot()
    {
        canShoot = true;
    }

    
}