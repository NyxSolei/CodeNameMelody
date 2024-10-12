using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class FallingTrap : Trap
{
    private int trapDamage = 5;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
            Debug.Log("LAMA ATA LO NOFEL");
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
