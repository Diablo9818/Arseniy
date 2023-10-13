using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float damage;

    public void DestroyThisIn(float time)
    {
        Destroy(gameObject, time);
    }
}
