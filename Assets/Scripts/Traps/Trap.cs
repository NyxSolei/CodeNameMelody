using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public abstract class Trap : MonoBehaviour, IDamagable
{
    public LayerMask whatIDamagable;

    public int trapHealth = 10;
    public abstract void ApplyDamage(IDamagable damagable);

    public virtual void TakeDamage(int damageAmount)
    {
        trapHealth -= damageAmount;

        if (trapHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
