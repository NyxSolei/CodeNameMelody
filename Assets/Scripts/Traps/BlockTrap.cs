using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageInterface;

public class BlockTrap : AbstractTrap
{
    private static BlockTrap _instance;
    public static BlockTrap Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BlockTrap();
            }
            return _instance;
        }
    }

    private string _trapType = "block";
    public override void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(this.GetTrapDamage());
    }

    public override string GetTrapType()
    {
        return this._trapType;
    }
}
