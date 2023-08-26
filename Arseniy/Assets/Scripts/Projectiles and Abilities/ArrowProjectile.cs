using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    public static event EventHandler OnArrowHit;

    Crossbow crossbow;
    private string NAME_OF_WEAPON = "Ballista";
    private bool hasEntered = false;

    private void Start()
    {
        crossbow = GameObject.Find("Crossbow").GetComponent<Crossbow>();
        damage = crossbow.projectileDamage;
        DestroyThisIn(10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && !hasEntered) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);

            if (enemy.GetComponent<Shield>() == null && enemy.GetComponent<StoneEnemy>() == null) {
                OnArrowHit?.Invoke(this, EventArgs.Empty);
            }
            hasEntered = true;
            Destroy(gameObject);
        }
        

        if (collision.tag == "Border")
        {
            Destroy(gameObject);

        }

        if (collision.tag == "Rock")
        {
            Destroy(collision.gameObject);

        }
    }
}
