using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPiano : PlayerControlsAbstract, ICooldown
{
    private static PlayerControlPiano _instance;
    public static PlayerControlPiano instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerControlPiano();
            }
            return _instance;
        }
    }

    public float CooldownDuration { get; set; }
    public float LastCooldownTime { get; set; }

    private string _playerType = "piano";
    private int _shieldPowerMax = 10;
    private int _currentShieldPoints;
    private int __minShieldPoints = 0;
    private float _pianoCooldownDuration = 2f;
    private bool _isShieldSet = false;
    public override string GetPlayerType()
    {
        return this._playerType;
    }

    public int GetMaxShieldPower()
    {
        return this._shieldPowerMax;
    }
    public void SetCurrentShieldPower()
    {
        this._currentShieldPoints = this.GetShieldPower();
    }
    public int GetCurrentShieldPower()
    {
        return this._currentShieldPoints;
    }
    public int GetMinShieldPower()
    {
        return this.__minShieldPoints;
    }
    public void SetShieldToMin()
    {
        this.SetShieldPower(this.GetMinShieldPower());
        this.SetCurrentShieldPower();
        this._isShieldSet = false;
    }
    public void TakeDamageWithShield(int damageTaken)
    {
        if (this.GetCurrentShieldPower() > this.GetMinShieldPower())
        {
            if (this.GetCurrentShieldPower() - damageTaken < this.GetMinShieldPower())
            {
                this.TakeDamage(Mathf.Abs(this.GetShieldPower() - damageTaken));
                this.SetShieldToMin();
            }
            else
            {
                this.SetShieldPower(this.GetShieldPower() - damageTaken);
            }
        }
        else
        {
            this.TakeDamage(damageTaken);
        }

        ShieldDisplay.instance.UpdateShieldDisplay();
    }
    public override void UseAbility()
    {
        // shield is active as long as the player holds the ability button
        // shield expires if: a. player stops holding the button b. player took damage equal to the shield amount
        // need to set cooldown time
        if (!this._isShieldSet)
        {
            this.SetShieldPower(this._shieldPowerMax);
            this._isShieldSet = true;
            ShieldDisplay.instance.InstantiateNewShieldDisplay();
        }
        

        LastCooldownTime = -Mathf.Infinity;
        CooldownDuration = this._pianoCooldownDuration;
    }

    public bool CooldownComplete()
    {
        return Time.time >= LastCooldownTime + CooldownDuration;
    }

    public void StartCooldown()
    {
        LastCooldownTime = Time.time;
    }

    public void RemoveShield()
    {
        this.SetShieldToMin();
    }

    public bool GetIsShieldSet()
    {
        return this._isShieldSet;
    }
}
