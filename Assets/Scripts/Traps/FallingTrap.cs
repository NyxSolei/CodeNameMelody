using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class FallingTrap : AbstractTrap
{
    private static FallingTrap _instance;
    public static FallingTrap instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FallingTrap();
            }
            return _instance;
        }
    }

    private string _trapType = "falling";
    public override void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(this.GetTrapDamage());
    }

    public override string GetTrapType()
    {
        return this._trapType;
    }
}
