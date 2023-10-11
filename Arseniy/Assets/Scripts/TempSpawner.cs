using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Events;

public class TempSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float forBigMinY = -0.27f;
    [SerializeField] private float forBigMaxY = 2.97f;    
    [SerializeField] private float forSmallMinY = -0.27f;
    [SerializeField] private float forSmallMaxY = -1.9f;
    [SerializeField] private int enemiesKillCountToOpenPanel;
    [SerializeField] SkillPanel panel;
    [SerializeField] public int fastEnemyNumber { get; private set; } = 1;
    [SerializeField] public int flyingEnemyNumber { get; private set; } = 2;
    [SerializeField] public int shieldEnemyNumber { get; private set; } = 3;
    [SerializeField] public int golemEnemyNumber { get; private set; } = 4;

    [SerializeField] private int fastEnemiesCount;
    [SerializeField] private int flyingEnemiesCount;
    [SerializeField] private int shieldEnemiesCount;
    [SerializeField] private int golemEnemiesCount;

    [SerializeField] private float _secondsBetweenSpawn;

    private int currentEnemies = 0;
    private float nextSpawnTime = 10f;
    private float _elapsedTime = 0;
    private bool isGolemSpawned = false;
    private int enemyKillCount;

    private void Start()
    {
        _elapsedTime = nextSpawnTime;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= nextSpawnTime)
        {
            if (currentEnemies < maxEnemies)
            {
                if (fastEnemiesCount >= 10 && flyingEnemiesCount >= 5 && shieldEnemiesCount >= 3 && !isGolemSpawned)
                {
                    SpawnEnemy("StoneEnemy", forBigMinY, forBigMaxY);
                    isGolemSpawned = true;
                    currentEnemies += golemEnemyNumber;
                    _elapsedTime = 0;

                    fastEnemiesCount = 0;
                    flyingEnemiesCount = 0;
                    shieldEnemiesCount = 0;
                }

                if (fastEnemiesCount >= 10 && flyingEnemiesCount >= 5 && !isGolemSpawned)
                {
                    SpawnEnemy("Enemy With Shield", forBigMinY, forBigMaxY);
                    currentEnemies += shieldEnemyNumber;
                    _elapsedTime = 0;
                }

                if (fastEnemiesCount >= 5 && !isGolemSpawned)
                {
                    SpawnEnemy("FlyingEnemy", forSmallMinY, forSmallMaxY);
                    currentEnemies += flyingEnemyNumber;
                    _elapsedTime = 0;
                }

                if (!isGolemSpawned)
                {
                    SpawnEnemy("Fast Enemy", forSmallMinY, forSmallMaxY);
                    currentEnemies += fastEnemyNumber;
                    _elapsedTime = 0;
                }
            }
        }

        if(enemyKillCount >= enemiesKillCountToOpenPanel)
        {
            ShowAbilityPanel();
            enemyKillCount = 0;
        }

        Debug.Log("enemyKillCount: " + enemyKillCount);
    }

    public void IncreaseKilledEnemyCount()
    {
        enemyKillCount++;
    }

    public void ShowAbilityPanel()
    {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
        Debug.Log("Ready");
    }

    public void IncreaseEnemyPower()
    {
        foreach (var enemy in enemyPrefabs)
        {
            enemy.IncreasePower();
        }
    }

    public void KilledFastEnemiesIncrease()
    {
        fastEnemiesCount++;
    }

    public void GolemUnspawned()
    {
        isGolemSpawned = false;
    }

    public void KilledFlyingEnemiesIncrease()
    {
        flyingEnemiesCount++;
    }
    
    public void KilledShieldEnemiesIncrease()
    {
        shieldEnemiesCount++;
    }

    public void DecreaseEnemiesCount(int enemyNumber)
    {
        currentEnemies-=enemyNumber;
    }

    private void SpawnEnemy(string tag, float minY, float maxY)
    {
        GameObject enemyPrefab = GetPrefabByTag(tag);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private GameObject GetPrefabByTag(string tag)
    {
        foreach (Enemy prefab in enemyPrefabs)
        {
            if (prefab.CompareTag(tag))
            {
                return prefab.gameObject;
            }
        }

        return null;
    }
}