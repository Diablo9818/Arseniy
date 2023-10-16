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
            var tempSpawner = FindObjectOfType<TempSpawner>();
            tempSpawner.KilledFastEnemiesIncrease();
            gameManager.UpdateScore(score);
            tempSpawner.DecreaseEnemiesCount(tempSpawner.fastEnemyNumber);
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 1);
    }

}
