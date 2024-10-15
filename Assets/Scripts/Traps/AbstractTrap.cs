using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : DamageInterface.IDamagable
{
    private int _trapHealth;
    private int _trapDamage;
    LayerMask whatIDamage;
    private int _zeroHealth = 0;

    public void SetLayerForCheck(LayerMask layer)
    {
        this.whatIDamage = layer;
    }
    public int GetZeroHealth()
    {
        return this._zeroHealth;
    }
    public void SetZeroHealth()
    {
        this.SetHealth(this.GetZeroHealth());
    }
    public void SetTrapDamage(int damage)
    {
        this._trapDamage = damage;
    }
    public int GetTrapDamage()
    {
        return this._trapDamage;
    }
    public void SetHealth(int hp)
    {
        this._trapHealth = hp;
    }
    public int GetHealth()
    {
        return this._trapHealth;
    }
    private bool IsInLayer(GameObject gameObject, LayerMask layer)
    {
        return (layer.value & (1 << gameObject.layer)) != 0;
    }
    public abstract void ApplyDamage(DamageInterface.IDamagable damagable);
    public void Die()
    {
        this.SetZeroHealth();
    }
    public void TakeDamage(int damageAmount)
    {
        this.SetHealth(this.GetHealth() - damageAmount);
    }

}
