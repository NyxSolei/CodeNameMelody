using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class SpikedTrap : Trap
{
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Player player = collision.gameObject.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        ApplyDamage(player);
    //    }
    //}

    public override void ApplyDamage(IDamagable damagable)
    {
        Debug.Log("Player stepped on the spiked trap!");
        if (damagable != null)
        {
            damagable.TakeDamage(10);
        }
    }
}
