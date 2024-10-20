using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInterface : MonoBehaviour
{
    public interface IDamagable
    {
        void TakeDamage(int damageAmount);
        void Die();
    }
}

