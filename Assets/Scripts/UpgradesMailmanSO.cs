using UnityEngine;

[CreateAssetMenu()]
public class UpgradesMailmanSO : ScriptableObject
{
    public FireGun.DamageLevel FlamethrowerDamageLevel;
    public FireGun.DotDamageLevel FlamethrowerDotDamageLevel;
    public FireGun.DotDurationLevel FlamethrowerDotDurationLevel;

    public Crossbow.DamageLevel CrossbowDamageLevel;

    public Mortar.DamageLevel MortarDamageLevel;

    public bool isFireGunAbilityBought;
    public bool isCrossbowAbilityBought;
    public bool isMortarAbilityBought;
}
