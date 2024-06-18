using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int[] enemyWaveAmounts = { 3, 5 };
    public float timeBetweenWaves = 10f;

    public GameObject[] enemies;

    private int currentWave = 0;

    private void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        int enemyAmountToSpawn = enemyWaveAmounts[currentWave];
        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            int enemyTypeIndex = Random.Range(0, enemies.Length);
            GameObject enemyToSpawn = enemies[enemyTypeIndex];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
        currentWave++;

        if (currentWave <= enemyWaveAmounts.Length - 1)
            Invoke("SpawnWave", timeBetweenWaves);
    }

}