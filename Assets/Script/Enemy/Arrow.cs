using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Transform player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 1.3f)
        {
            player.gameObject.GetComponent<PlayerStats>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

}