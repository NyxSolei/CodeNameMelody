using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : AbstractTrap
{
    private static GroundTrap _instance;
    public static GroundTrap instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GroundTrap();
            }
            return _instance;
        }
    }

    private string _trapType = "ground";

    public override void ApplyDamage(DamageInterface.IDamagable damagable)
    {
        damagable.TakeDamage(this.GetTrapDamage());
    }

    public override string GetTrapType()
    {
        return this._trapType;
    }


}
