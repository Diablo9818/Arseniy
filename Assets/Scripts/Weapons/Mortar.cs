using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;

public class Mortar : Weapon
{
    bool isReloading = false;

    public event EventHandler OnAbilityAction;

    public static event EventHandler OnMortarShot;

    GameObject sentProjectile;
    [HideInInspector] public Vector3 target;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileDamage;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] GameObject superProjectile;
    [SerializeField] float coolDownTime;
    [SerializeField] int superShootsCount = 3;
    [SerializeField] public float superProjectileSpeed = 10f;
    [SerializeField] private MortarAbilityButton abilityButton;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite ReloadingSprite;
    [SerializeField] int currDamageLevel;

    bool isSuperPowerActivated = false;

    public static void ResetStaticData()
    {
        OnMortarShot = null;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ResetStaticData();
    }

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();


        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }
    
    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Mortar)
        {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);
            if (Input.GetMouseButton(1))
            {
                Crosshair();
                CrosshairUnHide();
                Aim();
                if (!isReloading)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        target = crosshair.transform.position;

                        if (isSuperPowerActivated && superShootsCount > 0)
                        {
                            SuperShoot();
                        }
                        else
                        {
                            Shoot();
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                CrosshairHide();
            }

            
        } else {
            abilityButtonUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (superShootsCount == 0)
        {
            isSuperPowerActivated = false;
            StartCoroutine(Cooldown());
        }
    }

    public void ActivatePower()
    {
        isSuperPowerActivated = true;
    }

    public override void Shoot()
    {
        if (!isReloading)
        {
            GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;

            OnMortarShot?.Invoke(this, EventArgs.Empty);

            spriteRenderer.sprite = ReloadingSprite;
            StartCoroutine(Reload());
        }
    }

    public  void SuperShoot()
    {
        if (!isReloading)
        {
            OnAbilityAction?.Invoke(this, EventArgs.Empty);
            GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
            superShootsCount--;
            StartCoroutine(Reload());

            OnMortarShot?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        spriteRenderer.sprite = defaultSprite;
        isReloading = false;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(coolDownTime);

        superShootsCount = 3;
    }

    public float GetAbilityCooldown()
    {
        return coolDownTime;
    }

    public void SetDamageLevel(int newLevel)
    {
        currDamageLevel = newLevel;
    }

    public void SetDamage(float newDamage, int newLevel)
    {
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/Weapons/Mortar.cs
        switch (currentDamageLevel) {
            case DamageLevel.Level1:
                projectileDamage = mortarDamageLevel1;
                break;
            case DamageLevel.Level2:
                projectileDamage = mortarDamageLevel2;
                break;
            case DamageLevel.Level3:
                projectileDamage = mortarDamageLevel3;
                break;
            case DamageLevel.Level4:
                projectileDamage = mortarDamageLevel4;
                break;
        }
=======
        projectileDamage = newDamage;
        currDamageLevel = newLevel;
>>>>>>> Stashed changes:Assets/Scripts/Weapons/Mortar.cs
    }

    public void UpgradeDamageLevel()
    {
        projectileDamage += projectileDamage * 0.25F * currDamageLevel;
        currDamageLevel++;
    }
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/Weapons/Mortar.cs
    public DamageLevel GetCurrentDamageLevel()
    {
        return currentDamageLevel;
=======

    public int GetCurrentDamageLevel()
    {
        return currDamageLevel;
>>>>>>> Stashed changes:Assets/Scripts/Weapons/Mortar.cs
    }

    public float GetCurrentDamage()
    {
        return projectileDamage;
    }
}