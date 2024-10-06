using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlGuitar : PlayerControlsAbstract
{
    private static PlayerControlGuitar _instance;
    public static PlayerControlGuitar instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerControlGuitar();
            }
            return _instance;
        }
    }

    private float _fireCooldown = 0.5f;
    private float _nextFireTime = 0f;
    private string _playerType = "guitar";

    public override string GetPlayerType()
    {
        return this._playerType;
    }

    // to do in main player code the get transform on the gameobject of the player so the 
    // projectile will be hurled from the player's location in the map

    public override void UseAbility()
    {
        ProjectileManager.instance.FireProjectile();
    }
}
