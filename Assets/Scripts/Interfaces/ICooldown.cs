using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldown 
{
    bool CooldownComplete();

    void StartCooldown();

    float CooldownDuration { get; set; }

    float LastCooldownTime { get; set; }
}
