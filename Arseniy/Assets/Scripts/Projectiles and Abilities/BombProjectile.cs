using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BombProjectile : Projectile
{
    public static event EventHandler OnBombExplosion;

    Mortar mortar;
    Vector3 projectileTarget;
    private string NAME_OF_WEAPON = "Mortar";

    private bool once = true;

    
    private void Start()
    {
        mortar = GameObject.Find("Mortar").GetComponent<Mortar>();
        damage = mortar.projectileDamage;
        projectileTarget = mortar.target;
    }

    private void Update()
    {
        if(mortar != null) 
        {
            if(gameObject.transform.position != projectileTarget) 
            {
                transform.position = Vector3.MoveTowards(transform.position, projectileTarget, mortar.projectileSpeed * Time.deltaTime);
            }
            else
            { 
                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                transform.GetComponent<Animator>().SetTrigger("Explosion");
                GetComponent<CircleCollider2D>().enabled = true;

                if (once) {
                    OnBombExplosion?.Invoke(this, EventArgs.Empty);
                    once = false;
                }
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
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
