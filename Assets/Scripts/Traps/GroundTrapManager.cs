using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrapManager : MonoBehaviour, DamageInterface.IDamagable
{
    private float _damageTimer = 0f;
    private int _groundTrapDamage = 2;
    private float _secondInterval = 1f;
    private float _timerReset = 0f;
    private int _groundTrapHealth = 40;

    private void Start()
    {
        GroundTrap.instance.SetTrapDamage(this._groundTrapDamage);
        SetTrapHealth();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GroundTrap.instance.IsInLayer(collision.gameObject))
        {
            // Check if the object implements the IDamagable interface
            DamageInterface.IDamagable damagable = collision.GetComponent<DamageInterface.IDamagable>();

            if (damagable != null)
            {
                this._damageTimer += Time.deltaTime;
                if (this._damageTimer >= this._secondInterval)
                {
                    GroundTrap.instance.ApplyDamage(damagable);
                    this._damageTimer = this._timerReset;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GroundTrap.instance.IsInLayer(collision.gameObject))
        {
            this._damageTimer = this._timerReset;
        }
    }

    private void SetTrapHealth()
    {
        GroundTrap.instance.SetHealth(this._groundTrapHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        GroundTrap.instance.TakeDamage(damageAmount);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public int GetHealth()
    {
        return GroundTrap.instance.GetHealth();
    }
}
