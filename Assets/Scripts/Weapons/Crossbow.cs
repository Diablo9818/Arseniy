using System.Collections;
using UnityEngine;
using System;

public class Crossbow : Weapon
{
    public Action OnAbilityAction;
    public Action OnBallistaDefaultShot;
    public Action OnBallistaSuperShot;

    public SkillManager skillsManager;

    [HideInInspector] public Vector3 target;
    //GameObject sentProjectile;
    //bool isSentProjectileDropped = true;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileDamage;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

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

    [SerializeField] private Transform FirstProjectileSpawnerTransform;
    [SerializeField] private Transform SecondProjectileSpawnerTransform;
    [SerializeField] private Transform ThirdProjectileSpawnerTransform;
    [SerializeField] private Transform FourthProjectileSpawnerTransform;

    public enum DamageLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillsManager = FindObjectOfType<SkillManager>();
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
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.5f, 0f), projectileSpawnerTransform.rotation);
            CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.5f, 0f), projectileSpawnerTransform.rotation);
        }
        else if (skillsManager.crossbowPlusOneArrow)
        {
            CreateProjectile(projectile, FirstProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, 30));
        }
        else if (skillsManager.crossbowPlusTwoArrow)
        {
            CreateProjectile(projectile, SecondProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, -30));
        }
        else if (skillsManager.crossbowPlusThreeArrow)
        {
            CreateProjectile(projectile, ThirdProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, 60));
        }
        else if (skillsManager.crossbowPlusFourArrow)
        {
            CreateProjectile(projectile, FourthProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, -60));
        }
        else
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position, projectileSpawnerTransform.rotation);
        }

        isReloading = true;
        StartCoroutine(Reload());

        OnBallistaDefaultShot?.Invoke();
    }

    private void CreateProjectile(GameObject projectile, Vector3 spawnPosition, Quaternion rotation)
    {
        var sentProjectile = GameObject.Instantiate(projectile, spawnPosition, rotation);
        sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * projectileSpeed, ForceMode2D.Impulse);

        if (sentProjectile.TryGetComponent(out ArrowProjectile arrowProjectile))
        {
            arrowProjectile.damage = projectileDamage;
        }
    }

    public void SuperShoot()
    {
        if (!isReloading)
        {
            OnAbilityAction?.Invoke();
            var sentProjectile = GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * superProjectileSpeed, ForceMode2D.Impulse);
            isReloading = true;
            superShootsCount--;
            StartCoroutine(Reload());

            OnBallistaSuperShot?.Invoke();
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
    public void SetDamageLevel(DamageLevel level)
    {
        currentDamageLevel = level;
    }
}
