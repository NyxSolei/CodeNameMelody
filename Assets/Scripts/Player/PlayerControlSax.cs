using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSax : PlayerControlsAbstract
{
    private static PlayerControlSax _instance;
    public static PlayerControlSax instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerControlSax();
            }
            return _instance;
        }
    }
    private string _playerType = "sax";
    private float _highjumpForce = 30f;

    public override string GetPlayerType()
    {
        return this._playerType;
    }

    public override void UseAbility()
    {
        if (!this.IsJumpForceSet())
        {
            PlayerControls.instance.SetPlayerCurrentJumpForce(this._highjumpForce);
        }

        PlayerControls.instance.Jump();
    }

    public bool IsJumpForceSet()
    {
        if (this.GetJumpForce() == this._highjumpForce)
        {
            return true;
        }
        return false;
    }
}
