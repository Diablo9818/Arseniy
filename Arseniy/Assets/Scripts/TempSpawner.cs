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
    [SerializeField] public int fastEnemyNumber { get; private set; } = 1;
    [SerializeField] public int flyingEnemyNumber { get; private set; } = 2;
    [SerializeField] public int shieldEnemyNumber { get; private set; } = 3;
    [SerializeField] public int golemEnemyNumber { get; private set; } = 4;

    [SerializeField] private int WaveEnemiesAmount;
    private int currWaveEnemiesAmount;
    [SerializeField] private int golemEnemiesCount;

    [SerializeField] private float _secondsBetweenSpawn;

    private int currentEnemies = 0;
    private float nextSpawnTime = 10f;
    private float _elapsedTime = 0;
    private bool isGolemSpawned = false;

    public static int WaveNumber;

    private void Awake()
    {
       int TempWave = PlayerPrefs.GetInt("SavedWave", 1);
        if(TempWave != 1)
        {

        }
    }

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
                int EnemyRandom = Random.Range(1, 10);

                if (currWaveEnemiesAmount >= WaveEnemiesAmount && !isGolemSpawned)
                {
                    SpawnEnemy("StoneEnemy", forBigMinY, forBigMaxY);
                    isGolemSpawned = true;
                    currentEnemies += golemEnemyNumber;
                    _elapsedTime = 0;

                    currWaveEnemiesAmount = 0;
                }

                if (EnemyRandom == 9)
                {
                    SpawnEnemy("Enemy With Shield", forBigMinY, forBigMaxY);
                    currentEnemies += shieldEnemyNumber;
                    _elapsedTime = 0;
                }

                if (EnemyRandom > 5 && EnemyRandom < 9)
                {
                    SpawnEnemy("FlyingEnemy", forSmallMinY, forSmallMaxY);
                    currentEnemies += flyingEnemyNumber;
                    _elapsedTime = 0;
                }

                if (EnemyRandom <= 5)
                {
                    SpawnEnemy("Fast Enemy", forSmallMinY, forSmallMaxY);
                    currentEnemies += fastEnemyNumber;
                    _elapsedTime = 0;
                }
            }
        }
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/TempSpawner.cs
=======

        if(enemyKillCount >= enemiesKillCountToOpenPanel)
        {
            ShowAbilityPanel();
            enemyKillCount = 0;
        }
    }

    public void IncreaseKilledEnemyCount()
    {
        enemyKillCount++;
        currWaveEnemiesAmount++;
    }

    public void ShowAbilityPanel()
    {
        panel.gameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Ready");
>>>>>>> Stashed changes:Assets/Scripts/TempSpawner.cs
    }

    public void IncreaseEnemyPower()
    {
        foreach (var enemy in enemyPrefabs)
        {
            enemy.IncreasePower();
        }
    }
    public void GolemUnspawned()
    {
        isGolemSpawned = false;
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

    public void NextWave()
    {
        WaveNumber++;
        IncreaseEnemyPower();
        if(WaveNumber % 5 == 0)
        {
            int TempWave = PlayerPrefs.GetInt("SavedWave");
            if (TempWave != 1)
            {
                PlayerPrefs.SetInt("SavedWave", ++TempWave);
            }
        }
    }
}