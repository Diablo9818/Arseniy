using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneEnemy : Enemy
{

    [Header("----------DO !NOT! TOUCH----------")]
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private Transform _throwPoint;

    [Header("----------MOVEMENT PROPERTIES----------")]
    [SerializeField] private int _stopSteps = 600; 
    [SerializeField] private float _stopDuration = 7f;

    private float defoldDamage = 214.4531f;
    private float defoldHealth = 100f;

    private int _steps = 0; 
    private bool _isStopping = false; 
    private float _stopTime = 0f;

    private void Update()
    {
        healthBar.value = health;
        if (!_isStopping)
        {
            Move();
            _steps++;

            if (_steps >= _stopSteps)
            {
                animator.SetBool("IsThrowing", true);
                Stop();
                animator.SetBool("IsThrowing", false);
            }
        }
        else
        {
            if (Time.time >= _stopTime)
            {
                _isStopping = false;
            }
        }

        CheckDeath();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            speed = 0;
        }
    }

    private void Stop()
    {
        _isStopping = true;
        _steps = 0;
        _stopTime = Time.time + _stopDuration;
        ThrowRock();
    }

    private void ThrowRock()
    {
        GameObject rock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();
        rockRigidbody.AddForce(Vector2.left * 500f);
    }

    public override void Die()
    {
        animator.SetBool("IsDead", true);

        if (once)
        {
            gameManager.UpdateScore(score);
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/EnemiesScripts/StoneEnemy.cs
            enemySpawner.DecreaseEnemiesCount(enemySpawner.golemEnemyNumber);
            enemySpawner.GolemUnspawned();
            enemySpawner.IncreaseEnemyPower();
=======
            var tempSpawner = FindObjectOfType<TempSpawner>();
            tempSpawner.DecreaseEnemiesCount(tempSpawner.golemEnemyNumber);
            tempSpawner.GolemUnspawned();
            tempSpawner.IncreaseKilledEnemyCount();
            tempSpawner.NextWave();
>>>>>>> Stashed changes:Assets/Scripts/EnemiesScripts/StoneEnemy.cs
            once = false;
        }

        Destroy(gameObject, 1.5f);
    }
}
