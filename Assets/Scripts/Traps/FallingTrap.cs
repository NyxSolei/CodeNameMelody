using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class FallingTrap : Trap
{
    public float fallSpeed = 5f; 
    private bool isFalling = false; 
    private void Update()
    {
        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            isFalling = true; 
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        Debug.Log("Falling trap has fallen on the player!");
        if (damagable != null)
        {
            damagable.TakeDamage(5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && isFalling)
        {
            ApplyDamage(player); 

            Die(); 
        }
    }
}
