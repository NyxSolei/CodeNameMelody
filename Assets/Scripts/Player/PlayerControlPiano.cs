using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPiano : PlayerControlsAbstract
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

    private string _playerType = "piano";
    private int _shieldPowerMax = 10;
    private int _currentShieldPoints;
    private int _minSheildPoints = 0;
    public override string GetPlayerType()
    {
        return this._playerType;
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
        return this._minSheildPoints;
    }
    public void SetShieldToMin()
    {
        this.SetShieldPower(this.GetMinShieldPower());
        this.SetCurrentShieldPower();
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
    }
    public override void UseAbility()
    {
        // shield is active as long as the player holds the ability button
        // shield expires if: a. player stops holding the button b. player took damage equal to the shield amount
        // need to set cooldown time
        this.SetShieldPower(this._shieldPowerMax);
    }
}
