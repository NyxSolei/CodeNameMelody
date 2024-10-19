using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrapManager : MonoBehaviour
{
    private int _blockTrapDamage = 0;
    void Start()
    {
        this.SetBlockTrapDamage();
    }

    private void SetBlockTrapDamage()
    {
        BlockTrap.instance.SetTrapDamage(this._blockTrapDamage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageInterface.IDamagable damagable = collision.gameObject.GetComponent<DamageInterface.IDamagable>();
        if (damagable != null)
        {
            // ensures that the collision object is in layer

            if (BlockTrap.instance.IsInLayer(collision.gameObject))
            {
                // applies damage
                BlockTrap.instance.ApplyDamage(damagable);
            }

        }
    }
}
