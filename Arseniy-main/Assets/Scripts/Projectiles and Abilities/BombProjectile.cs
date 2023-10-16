using System;
using UnityEngine;

public class BombProjectile : Projectile
{
    public Action OnBombExplosion;

    Mortar mortar;
    Vector3 projectileTarget;
    private string NAME_OF_WEAPON = "Mortar";

    private bool once = true;


    private void Start()
    {
        mortar = FindObjectOfType<Mortar>();
        damage = mortar.projectileDamage;
        projectileTarget = mortar.target;
    }

    private void Update()
    {
        if (mortar != null)
        {
            if (gameObject.transform.position != projectileTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, projectileTarget, mortar.projectileSpeed * Time.deltaTime);
            }
            else
            {
                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                transform.GetComponent<Animator>().SetTrigger("Explosion");
                GetComponent<CircleCollider2D>().enabled = true;

                if (once)
                {
                    OnBombExplosion?.Invoke();
                    once = false;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            if (collision.GetComponent<ShieldEnemy>() != null)
            {
                collision.GetComponent<ShieldEnemy>().TakeDamage(damage, NAME_OF_WEAPON, gameObject);
            }
            else
            {
                enemy.TakeDamage(damage, NAME_OF_WEAPON);
            }
        }
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
