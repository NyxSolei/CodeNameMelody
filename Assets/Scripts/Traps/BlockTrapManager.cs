using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrapManager : MonoBehaviour, DamageInterface.IDamagable
{
    private int _blockTrapDamage = 0;
    private int _blockTrapHealth = 10;
    void Start()
    {
        this.SetBlockTrapDamage();
        this.SetBlockTrapHealth();
    }

    private void SetBlockTrapHealth()
    {
        BlockTrap.Instance.SetHealth(this._blockTrapHealth);
    }
    private void SetBlockTrapDamage()
    {
        BlockTrap.Instance.SetTrapDamage(this._blockTrapDamage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageInterface.IDamagable damagable = collision.gameObject.GetComponent<DamageInterface.IDamagable>();
        if (damagable != null)
        {
            // ensures that the collision object is in layer

            if (BlockTrap.Instance.IsInLayer(collision.gameObject))
            {
                // applies damage
                BlockTrap.Instance.ApplyDamage(damagable);
            }

        }
    }

    public int GetHealth()
    {
        return BlockTrap.Instance.GetHealth();
    }
    public void TakeDamage(int damageAmount)
    {
        BlockTrap.Instance.TakeDamage(damageAmount);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
