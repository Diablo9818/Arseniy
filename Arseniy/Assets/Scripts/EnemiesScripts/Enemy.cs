using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected bool once = true;
    [SerializeField] public Slider healthBar;
    [HideInInspector] public WallBehavior wall;
    protected float lastAttackTime;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isAttracted = false;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;
    [SerializeField] public int weight;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField]  protected int score;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected TempSpawner enemySpawner;
    [SerializeField] private AnimationClip walkAnimationClip;
    [SerializeField] private int coinsForDestroy;

    private Animation animationComponent;
    [SerializeField] protected Animator animator;



    [Header("----------DAMAGE RESIST----------")]
    [SerializeField] public float armor;
    [SerializeField] public float arrowDamageResist;
    [SerializeField] public float bombDamageResist;
    [SerializeField] public float fireDamageResist;
    [SerializeField] public GameObject centerObject;
    public float currentDamageResist;
    public const string BALLISTA = "Ballista";
    public const string MORTAR = "Mortar";
    public const string FIREGUN= "FireGun";

    float speedValue;

    [HideInInspector] public float damageReduce;
    [HideInInspector] public float actualDamage;

    private void OnDestroy()
    {
        Player.Instance.AddCoins(coinsForDestroy);
    }

    private void Awake()
    {
        wall = GameObject.Find("Wall").GetComponent<WallBehavior>();

        health = maxHealth;
        healthBar.maxValue = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //enemySpawner = GameObject.Find("EnemySpawnerTemp").GetComponent<TempSpawner>();

        animator = GetComponent<Animator>();
        speedValue = speed;
        animationComponent = gameObject.AddComponent<Animation>();
        animationComponent.AddClip(walkAnimationClip, "Walk");
    }

    private void Update()
    {
        if (!isAttacking && !isAttracted) 
        {
            Move();
        } 
        else if(isAttacking) 
        {
            Attack();
        }
        else if(isAttracted)
        {
            BeAttracted();
        }

        CheckDeath();
        
        healthBar.value = health;
    }

    protected void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public  void GetStunned()
    {
        speed = 0;
    }
    public void GetAttracted()
    {
        isAttracted = true;
    }

    public void BeAttracted()
    {
        Vector3 targetPosition = centerObject.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void GetUnattracted()
    {
        isAttracted = false;
    }

    public void GetUnstunned()
    {
        speed = speedValue;
    }

    public virtual void TakeDamage(float weaponDamage, string weaponName)
    {
        
        switch (weaponName) {
            case BALLISTA:
                currentDamageResist = arrowDamageResist;
                break;
            case MORTAR:
                currentDamageResist = bombDamageResist;
                break;
            case FIREGUN:
                currentDamageResist = fireDamageResist;
                break;
        }
            


        damageReduce = armor / (armor + 400);
        actualDamage = (weaponDamage * (1 - damageReduce)) * (1 - currentDamageResist);
        health -= actualDamage;

        healthBar.value = health;
    }
    
    public virtual void Die()
    {
        animator.SetBool("IsDead", true);

        if (once)
        {
            gameManager.UpdateScore(score);
            //enemySpawner.DecreaseEnemiesCount();
            once = false;
        }

        Destroy(gameObject, 3);
    }

    public virtual void Move()
    {
        //animationComponent.Play("Walk");
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wall" ) {
            isAttacking = true;
        }
    }

    public virtual void Attack()
    {
        if(Time.time - lastAttackTime < attackCooldown) {
            return;
        }
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public virtual int GetWeight()
    {
        return weight;
    }
}
