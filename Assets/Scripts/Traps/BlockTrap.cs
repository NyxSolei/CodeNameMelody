using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class BlockTrap : Trap
{
    public Collider2D blockCollider;

    private void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        blockCollider.isTrigger = false; 
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        Debug.Log("Player hit the trap");
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControls player = collision.gameObject.GetComponent<PlayerControls>();

            if (player != null && player.isAttacking)
            {
                TakeDamage(player.attackPower); 
            }
        }
    } */

    public override void TakeDamage(int damageAmount)
    {
        trapHealth -= damageAmount;

        if (trapHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("Trap destroyed!");
        Destroy(gameObject);
    }
}
