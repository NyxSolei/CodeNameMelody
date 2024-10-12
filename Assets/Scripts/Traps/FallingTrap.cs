using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class FallingTrap : Trap
{
    public float fallSpeed = 5f; 
    private bool isFalling = false;
    private int trapDamage = 5;
    private void Update()
    {
        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControls>().TakeDamage(trapDamage);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Player player = collision.gameObject.GetComponent<Player>();
    //    if (player != null && isFalling)
    //    {
    //        ApplyDamage(player); 

    //        Die(); 
    //    }
    //}

    public override void ApplyDamage(IDamagable damagable)
    {
        
    }
}
