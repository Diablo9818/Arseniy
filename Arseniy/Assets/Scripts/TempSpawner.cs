using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class TempSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float forBigMinY = -0.27f;
    [SerializeField] private float forBigMaxY = 2.97f;    
    [SerializeField] private float forSmallMinY = -0.27f;
    [SerializeField] private float forSmallMaxY = -1.9f;
    [SerializeField] private int fastEnemyNumber;
    [SerializeField] private int flyingEnemyNumber;
    [SerializeField] private int shieldEnemyNumber;
    [SerializeField] private int golemEnemyNumber;
    [SerializeField] private int fastEnemiesCount;
    [SerializeField] private int flyingEnemiesCount;
    [SerializeField] private int shieldEnemiesCount;
    [SerializeField] private int golemEnemiesCount;

    private int currentEnemies = 0;
    private float nextSpawnTime = 0f;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            if(Time.time >= nextSpawnTime)
            {
                SpawnEnemy("Fast Enemy", forSmallMinY, forSmallMaxY, fastEnemyNumber);

                if (fastEnemiesCount >= 5)
                {
                    SpawnEnemy("FlyingEnemy", forSmallMinY, forSmallMaxY, flyingEnemyNumber);
                }

                if(fastEnemiesCount >= 10 && flyingEnemiesCount >= 5)
                {
                    SpawnEnemy("Enemy With Shield", forBigMinY, forBigMaxY, shieldEnemyNumber);
                }

                if (fastEnemiesCount >= 10 && flyingEnemiesCount >= 5 && shieldEnemiesCount >= 3)
                {
                    SpawnEnemy("StoneEnemy", forBigMinY, forBigMaxY, golemEnemyNumber);
                }

                nextSpawnTime = Time.time + spawnDelay;
            }
        }

        if(currentEnemies > maxEnemies)
        {
            currentEnemies = maxEnemies;
        }

        Debug.Log(currentEnemies);
    }

    public void KilledFastEnemiesIncrease()
    {
        fastEnemiesCount++;
    }

    public void KilledFlyingEnemiesIncrease()
    {
        flyingEnemiesCount++;
    }    
    public void KilledShieldEnemiesIncrease()
    {
        shieldEnemiesCount++;
    }


    public void DecreaseEnemiesCount()
    {
        currentEnemies--;
    }

    private void SpawnEnemy(string tag, float minY, float maxY, int enemyNumber)
    {
        GameObject fastEnemyPrefab = GetPrefabByTag(tag);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(fastEnemyPrefab, spawnPosition, Quaternion.identity);

        currentEnemies+= enemyNumber;
    }

    private GameObject GetPrefabByTag(string tag)
    {
        foreach (GameObject prefab in enemyPrefabs)
        {
            if (prefab.CompareTag(tag))
            {
                return prefab;
            }
        }

        return null;
    }
}