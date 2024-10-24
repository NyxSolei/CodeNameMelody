using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapManager : MonoBehaviour, DamageInterface.IDamagable
{
    private Rigidbody2D _rb;
    private bool _hasFallen = false;
    private int _fallingTrapDamage = 10;
    private int _fallingTrapHealth = 20;
    
    void Start()
    {
        this._rb = GetComponent<Rigidbody2D>();
        this._rb.isKinematic = true;
        this.SetFallingTrapDamage();
        this.SetFallingTrapHealth();
    }

    private void SetFallingTrapHealth()
    {
        FallingTrap.instance.SetHealth(this._fallingTrapHealth);
    }
    private void SwitchHasFallen()
    {
        this._hasFallen = !this._hasFallen;
    }
    private bool GetHasFallen()
    {
        return this._hasFallen;
    }
    private void SetFallingTrapDamage()
    {
        FallingTrap.instance.SetTrapDamage(this._fallingTrapDamage);
    }
    private void TriggerFall()
    {
        this._rb.isKinematic = false;
        this.SwitchHasFallen();
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        DamageInterface.IDamagable damagable = collision.gameObject.GetComponent<DamageInterface.IDamagable>();
        if (damagable != null)
        {
            // ensures that the collision object is in layer

            if (FallingTrap.instance.IsInLayer(collision.gameObject))
            {
                // applies damage
                FallingTrap.instance.ApplyDamage(damagable);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FallingTrap.instance.IsInLayer(collision.gameObject) && !this.GetHasFallen())
        {
            // ensure that the *player* is under the trap
            DamageInterface.IDamagable damagable = collision.GetComponent<DamageInterface.IDamagable>();
            if (damagable != null)
            {
                // Make the trap fall
                TriggerFall();
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        FallingTrap.instance.TakeDamage(damageAmount);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public int GetHealth()
    {
        return FallingTrap.instance.GetHealth();
    }
}
