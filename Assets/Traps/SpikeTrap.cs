using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamagble;

public class SpikeTrap : Trap
{
    public int damageAmount = 10;

    public float bounceForce = 10f;

    public override void ApplyDamage(IDamagable damagable)
    {
       if (damagable != null)
       {
            damagable.TakeDamage(damageAmount);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*IDamagable target = collision.GetComponent<IDamagable>();

        if (target != null)
        {
            ApplyDamage(target);
        }*/

        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerBounce(collision.gameObject);
        }
    }

    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {   
            //reset player velocity
            rb.velocity = new Vector2 (rb.velocity.x, 0f);

            //apply bounce force
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
