using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamagble;

public abstract class Trap : MonoBehaviour, IDamagable
{
    public LayerMask whatIDamagable;

    public int trapHealth = 10;
    public abstract void ApplyDamage(IDamagable damagable);

    public void TakeDamage(int damageAmount)
    {
        trapHealth -= damageAmount;

        if (trapHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
