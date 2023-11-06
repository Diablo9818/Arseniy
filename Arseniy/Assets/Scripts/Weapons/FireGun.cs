using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireGun : Weapon
{
    public static event EventHandler OnAbilityAction;

    public static event EventHandler OnFireGunStartShooting;
    public static event EventHandler OnFireGunStopShooting;

    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] PolygonCollider2D fireCollider;
    private string NAME_OF_WEAPON = "FireGun";

    [Header("----------PROPERTIES----------")]
    [SerializeField] float damage;

    [Header("----------DOT PROPERTIES----------")]
    [SerializeField] public float dotDamage;
    [SerializeField] public int dotTicks;
    [SerializeField] public float dotDelay;
    [SerializeField] public float dotDuration;

    [Header("----------ULT PROPERTIES----------")]
    [SerializeField] private float percentOfSmallEnemies;
    [SerializeField] private float percentOfBigEnemies;
    [SerializeField] private float abilityCooldown;
    [SerializeField] private FiregunAbilityButton abilityButton;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private int currDotDamageLevel;
    [SerializeField] private int currDotDurationLevel;
    [SerializeField] private int currDamageLevel;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        ResetStaticData();
    }


    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }

    public static void ResetStaticData()
    {
        OnFireGunStartShooting = null;
        OnFireGunStopShooting = null;
        //OnAbilityAction = null;
    }

    private void Update()
    {

        if (playerScript.activeGun == Player.Weapon.FireGun) {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);
            if (Input.GetMouseButton(1)) {
                Aim();
                if (Input.GetMouseButtonDown(0)) {
                    Shoot();
                }
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
                StopShoot();
            }
        } else {
            abilityButtonUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (playerScript.activeGun != Player.Weapon.FireGun) {
            StopShoot();
        }
    }

    public override void Shoot()
    {
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/Weapons/FireGun.cs
=======
        if (skillsManager.fireGunleftFireEnable)
        {
            leftFireParticle.Play();
            leftfireCollider.enabled = true;
        }
        if (skillsManager.fireGunrightFireEnable)
        {
            rightFireParticle.Play();
            rightfireCollider.enabled = true;
        }
>>>>>>> Stashed changes:Assets/Scripts/Weapons/FireGun.cs

        fireParticle.Play();
        fireCollider.enabled = true;

        OnFireGunStartShooting?.Invoke(this, EventArgs.Empty);
    }

    public void StopShoot()
    {
        fireParticle.Stop();
        fireCollider.enabled = false;

        OnFireGunStopShooting?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            if (collision.tag == "Enemy With Shield") {
                if (collision.GetComponent<ShieldEnemy>().isShieldAlive == false) {
                    collision.GetComponent<ShieldEnemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                    if (!collision.GetComponent<FireDot>()) {
                        collision.gameObject.AddComponent<FireDot>();
                    }
                }
            } else {
                collision.GetComponent<Enemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                if (!collision.GetComponent<FireDot>()) {
                    collision.gameObject.AddComponent<FireDot>();
                }
            }
        }
    }

    public void FireGunAbility()
    {
        if (playerScript.activeGun == Player.Weapon.FireGun) {

            OnAbilityAction?.Invoke(this, EventArgs.Empty);
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies) {
                if (enemy.GetComponent<ShieldEnemy>() != null || enemy.GetComponent<StoneEnemy>() != null) {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfBigEnemies;
                } else if (enemy.GetComponent<Shield>() != null) {
                    // no damage to shield
                } else {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfSmallEnemies;
                }
            }

            Vector3 explosionPosition = new Vector3(2.5f, 0.25f, 0f);
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        }
    }
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/Weapons/FireGun.cs

    private void HandleUpgrading()
    {
        switch (currentDamageLevel) {
            case DamageLevel.Level1:
                damage = firegunDamageLevel1;
                break;
            case DamageLevel.Level2:
                damage = firegunDamageLevel2;
                break;
            case DamageLevel.Level3:
                damage = firegunDamageLevel3;
                break;
            case DamageLevel.Level4:
                damage = firegunDamageLevel4;
                break;
        }

        switch (currentDotDamageLevel) {
            case DotDamageLevel.Level1:
                dotDamage = dotDamageLevel1;
                break;
            case DotDamageLevel.Level2:
                dotDamage = dotDamageLevel2;
                break;
            case DotDamageLevel.Level3:
                dotDamage = dotDamageLevel3;
                break;
            case DotDamageLevel.Level4:
                dotDamage = dotDamageLevel4;
                break;
        }

        switch (currentDotDurationLevel) {
            case DotDurationLevel.Level1:
                dotTicks = dotDurationLevel1;
                break;
            case DotDurationLevel.Level2:
                dotTicks = dotDurationLevel2;
                break;
            case DotDurationLevel.Level3:
                dotTicks = dotDurationLevel3;
                break;
            case DotDurationLevel.Level4:
                dotTicks = dotDurationLevel4;
                break;
        }
    }

=======
>>>>>>> Stashed changes:Assets/Scripts/Weapons/FireGun.cs
    public float GetAbilityCooldown()
    {
        return abilityCooldown;
    }

    public void SetUpgrades(float Damage, int DamageLevel)
    {
        damage = Damage;
        dotDamage = damage * 0.05F;
        currDamageLevel = DamageLevel;
    }
    public void SetUpgrades(int DamageLevel)
    {
        currDamageLevel = DamageLevel;
    }


    public void UpgradeDamage()
    {
        damage += damage * 0.25F * currDamageLevel;
        dotDamage = damage * 0.05F;
        currDotDamageLevel++;
    }

    public int GetCurrentDotDurationLevel()
    {
        return currDotDurationLevel;
    }
    public int GetCurrentDamageLevel()
    {
        return currDamageLevel;
    }
    public int GetCurrentDotDamageLevel()
    {
        return currDotDamageLevel;
    }

    public float GetCurrentDamage()
    {
        return damage;
    }

    public float GetCurrentDotDuration()
    {
        return dotDuration;
    }

    public float GetCurrentDotDamage()
    {
        return dotDamage;
    }
}