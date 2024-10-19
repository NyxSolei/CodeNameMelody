using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool hasFallen = false;
    private int _fallingTrapDamage = 10;
    
    void Start()
    {
        this._rb = GetComponent<Rigidbody2D>();
        this._rb.isKinematic = true;
        this.SetFallingTrapDamage();
    }

    private void SetFallingTrapDamage()
    {
        FallingTrap.instance.SetTrapDamage(this._fallingTrapDamage);
    }
    private void TriggerFall()
    {
        this._rb.isKinematic = false;
        this.hasFallen = true;
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        DamageInterface.IDamagable damagable = collision.gameObject.GetComponent<DamageInterface.IDamagable>();
        if (damagable != null)
        {
            FallingTrap.instance.ApplyDamage(damagable); 
        }
    }

}
