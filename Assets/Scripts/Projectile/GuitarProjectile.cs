using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarProjectile : MonoBehaviour
{
    public float _speed = 90f;  
    public int _damage = 1;     
    public float _lifetime = 0.5f;


    Rigidbody2D rb;
    
    public void Launch(Vector2 direction)
    {
        
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = direction * _speed;
        Destroy(gameObject, _lifetime);
    }

    // Handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileManager.instance.IgnoreCollisionWithPlayer(this.gameObject);
        DamageInterface.IDamagable damagable = collision.gameObject.GetComponent<DamageInterface.IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(this._damage);
            Destroy(this.gameObject);
        }
    }



}
