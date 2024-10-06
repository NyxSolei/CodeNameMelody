using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class PlayerControlsAbstract
{
    private float _moveSpeed;
    private float _jumpForce;
    private int _health;
    private int _shieldPower;
    private int _maxHealthPoints;
    public void SetMoveSpeed(float speed)
    {
        this._moveSpeed = speed;
    }
    public float GetMoveSpeed()
    {
        return this._moveSpeed;
    }
    public void SetJumpForce(float force)
    {
        this._jumpForce = force;
    }
    public float GetJumpForce()
    {
        return this._jumpForce;
    }
    public void SetHealth(int health)
    {
        this._health = health;
    }
    public int GetHealth()
    {
        return this._health;
    }
    public abstract void UseAbility();
    public abstract string GetPlayerType();
    public void SetDefaultShieldPower()
    {
        this._shieldPower = 0;
    }
    public void SetShieldPower(int newPower)
    {
        this._shieldPower = newPower;
    }
    public int GetShieldPower()
    {
        return this._shieldPower;
    }
    public void TakeDamage(int damageAmount)
    {
        this.SetHealth(this.GetHealth() - damageAmount);
    }
    public int GetMaxHP()
    {
        return this._maxHealthPoints;
    }
    public void SetMaxHP(int healthPoints)
    {
        this._maxHealthPoints = healthPoints;
    }

}
