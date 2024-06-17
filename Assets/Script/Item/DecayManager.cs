using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayManager : MonoBehaviour
{
    [SerializeField] private float timeToDecay = 1f;
    [SerializeField] private GameObject decayedPrefab;

    private float currentTime;

    private void Start()
    {
        currentTime = timeToDecay;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            Decay();
        }
    }

    private void Decay()
    {
        Instantiate(decayedPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}