using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    private float defoldDamage = 3f;
    private float defoldHealth = 25f;

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public override void Die()
    {
        animator.SetBool("IsDead", true);

        if (once)
        {
            enemySpawner.KilledFastEnemiesIncrease();
            gameManager.UpdateScore(score);
            enemySpawner.DecreaseEnemiesCount(enemySpawner.fastEnemyNumber);
            once = false;
        }

        Destroy(gameObject, 1);
    }

}
