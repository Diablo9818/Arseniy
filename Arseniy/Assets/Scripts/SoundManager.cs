using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    private AudioSource audioSource;

    [SerializeField] private FireGun firegun;
    private AudioSource firegunAudioSource;

    [Header("----------VOLUME----------")]
    [Range(0f, 1f)]
    [SerializeField] private float ballistaShotVolume;
    [Range(0f, 1f)]
    [SerializeField] private float arrowHitVolume;
    [Range(0f, 1f)]
    [SerializeField] private float mortarShotVolume;
    [Range(0f, 1f)]
    [SerializeField] private float bombExplosionVolume;
    [Range(0f, 1f)]
    [SerializeField] private float firegunParticleVolume;
    [Range(0f, 1f)]
    [SerializeField] private float superBombMagnetEffectVolume;
    [Range(0f, 1f)]
    [SerializeField] private float firegunAbilityVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Crossbow.OnBallistaDefaultShot += Crossbow_OnBallistaDefaultShot;
        Crossbow.OnBallistaSuperShot += Crossbow_OnBallistaSuperShot;
        Mortar.OnMortarShot += Mortar_OnMortarShot;
        FireGun.OnFireGunStartShooting += FireGun_OnFireGunStartShooting;
        FireGun.OnFireGunStopShooting += FireGun_OnFireGunStopShooting;

        firegunAudioSource = firegun.GetComponent<AudioSource>();

        BombProjectile.OnBombExplosion += BombProjectile_OnBombExplostion;
        SuperBombProjectile.OnSuperBombActivation += SuperBombProjectile_OnSuperBombActivation;
        ArrowProjectile.OnArrowHit += ArrowProjectile_OnArrowHit;
        FireGun.OnAbilityAction += FireGun_OnAbilityAction;
    }

    private void FireGun_OnAbilityAction(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.firegunAbilitySound, firegunAbilityVolume);
    }

    private void ArrowProjectile_OnArrowHit(object sender, System.EventArgs e)
    {
        ArrowProjectile arrowProjectile = sender as ArrowProjectile;
        PlaySound(audioClipsRefsSO.arrowHitSound, arrowHitVolume);
    }

    private void BombProjectile_OnBombExplostion(object sender, System.EventArgs e)
    {
        BombProjectile bombProjectile = sender as BombProjectile;
        PlaySound(audioClipsRefsSO.bombExplosionSound, bombExplosionVolume);
    }

    private void SuperBombProjectile_OnSuperBombActivation(object sender, System.EventArgs e)
    {
        SuperBombProjectile superBombProjectile = sender as SuperBombProjectile;
        PlaySound(audioClipsRefsSO.bombExplosionSound, bombExplosionVolume);
        PlaySound(audioClipsRefsSO.superBombExplosionSound, superBombMagnetEffectVolume);
    }

    private void FireGun_OnFireGunStopShooting(object sender, System.EventArgs e)
    {
        firegunAudioSource.Pause();
    }

    private void FireGun_OnFireGunStartShooting(object sender, System.EventArgs e)
    {
        firegunAudioSource.clip = audioClipsRefsSO.firegunParticleSound;
        firegunAudioSource.volume = firegunParticleVolume;
        firegunAudioSource.Play();
    }

    private void Mortar_OnMortarShot(object sender, System.EventArgs e)
    {
        Mortar mortar = sender as Mortar;
        PlaySound(audioClipsRefsSO.mortarShotSound, mortarShotVolume);
    }

    private void Crossbow_OnBallistaSuperShot(object sender, System.EventArgs e)
    {
        Crossbow ballista = sender as Crossbow;
        PlaySound(audioClipsRefsSO.ballistaAbilityShotSound, ballistaShotVolume);
    }

    private void Crossbow_OnBallistaDefaultShot(object sender, System.EventArgs e)
    {
        Crossbow ballista = sender as Crossbow;
        PlaySound(audioClipsRefsSO.ballistaShotSound, ballistaShotVolume);
    }



    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], volume);
    }
}
