using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Crossbow : Weapon
{
    public event EventHandler OnAbilityAction;
    public static event EventHandler OnBallistaDefaultShot;
    public static event EventHandler OnBallistaSuperShot;

    public SkillManager skillsManager;

    [HideInInspector] public Vector3 target;
    //GameObject sentProjectile;
    //bool isSentProjectileDropped = true;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileDamage;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] private GameObject TwoArrow;
    [SerializeField] private GameObject ThreeArrow;
    [SerializeField] private GameObject FourArrow;
    [SerializeField] private GameObject FiveArrow;
    [SerializeField] private GameObject SixArrow;

    [SerializeField] GameObject superProjectile;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite superPowerSprite;
    [SerializeField] float coolDownTime;
    [SerializeField] int superShootsCount = 3;
    [SerializeField] public float superProjectileSpeed = 10f;
    [SerializeField] private CrossbowAbilityButton abilityButton;

    bool isReloading = false;
    bool isSuperPowerActivated = false;

    [Header("----------CROSSBOW UPGRADING----------")]
    [SerializeField] private DamageLevel currentDamageLevel;
    [SerializeField] private float crossbowDamageLevel1;
    [SerializeField] private float crossbowDamageLevel2;
    [SerializeField] private float crossbowDamageLevel3;
    [SerializeField] private float crossbowDamageLevel4;

    public enum DamageLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4
    }

    public static void ResetStaticData()
    {
        OnBallistaDefaultShot = null;
        OnBallistaSuperShot = null;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillsManager = FindObjectOfType<SkillManager>();

        ResetStaticData();
    }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Crossbow)
        {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);
            if (Input.GetMouseButton(1))
            {
                Crosshair();
                Aim();
                if (true)
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
                //CrosshairDisabled();
            }
        }
        else
        {
            abilityButtonUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (superShootsCount == 0)
        {
            isSuperPowerActivated = false;
            StartCoroutine(Cooldown());
        }

        HandleUpgrading();
    }

    public override void Shoot()
    {
        if (isReloading)
        {
            return;
        }
        if (skillsManager.crossbowCanDoubleShot)
        {
            CreateProjectile(projectile,projectileSpawnerTransform.position + new Vector3(0f,0.5f,0f));
            CreateProjectile(projectile,projectileSpawnerTransform.position + new Vector3(0f, -0.5f, 0f));
        }
        else if (skillsManager.crossbowPlusOneArrow)
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.2f, 0f));
        }
        else if (skillsManager.crossbowPlusTwoArrow)
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.2f, 0f));
        }
        else if (skillsManager.crossbowPlusThreeArrow)
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.4f, 0f));
        }
        else if (skillsManager.crossbowPlusFourArrow)
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.4f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.4f, 0f));
        }
        else if (skillsManager.crossbowPlusFiveArrow)
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.4f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.2f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.4f, 0f));
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.6f, 0f));
        }
        else
        {
            CreateProjectile(projectile,projectileSpawnerTransform.position);
        }

        isReloading = true;
        StartCoroutine(Reload());

        OnBallistaDefaultShot?.Invoke(this, EventArgs.Empty);
    }

    private void CreateProjectile(GameObject projectile,Vector3 spawnPosition)
    {
        var sentProjectile = GameObject.Instantiate(projectile, spawnPosition, transform.rotation);
        sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * projectileSpeed, ForceMode2D.Impulse);

        if(sentProjectile.TryGetComponent(out ArrowProjectile arrowProjectile))
        {
            arrowProjectile.damage = projectileDamage;
        }
    }

    public void SuperShoot()
    {
        if (!isReloading)
        {
            OnAbilityAction?.Invoke(this, EventArgs.Empty);
            var sentProjectile = GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * superProjectileSpeed, ForceMode2D.Impulse);
            isReloading = true;
            superShootsCount--;
            StartCoroutine(Reload());

            OnBallistaSuperShot?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }

    public void ActivatePower()
    {
        if (playerScript.activeGun == Player.Weapon.Crossbow)
        {
            isSuperPowerActivated = true;
            spriteRenderer.sprite = superPowerSprite;
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private IEnumerator Cooldown()
    {
        spriteRenderer.sprite = defaultSprite;

        yield return new WaitForSeconds(coolDownTime);

        superShootsCount = 3;
    }

    public float GetAbilityCooldown()
    {
        return coolDownTime;
    }
    public void SetDamage(float newDamage)
    {
        projectileDamage = newDamage;
    }
    private void HandleUpgrading()
    {
        switch (currentDamageLevel)
        {
            case DamageLevel.Level1:
                projectileDamage = crossbowDamageLevel1;
                break;
            case DamageLevel.Level2:
                projectileDamage = crossbowDamageLevel2;
                break;
            case DamageLevel.Level3:
                projectileDamage = crossbowDamageLevel3;
                break;
            case DamageLevel.Level4:
                projectileDamage = crossbowDamageLevel4;
                break;
        }
    }

    public void UpgradeDamageLevel()
    {
        if (currentDamageLevel == DamageLevel.Level4) return;

        currentDamageLevel++;
    }
    public DamageLevel GetCurrentDamageLevel()
    {
        return currentDamageLevel;
    }

    public float GetCurrentDamage()
    {
        return projectileDamage;
    }
}
