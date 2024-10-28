using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNote : MonoBehaviour , IItem
{
    private string _collectibleType = "HealthPickup";
    public int _healthRestore = 10;
    public void Collect()
    {
        Destroy(gameObject);
    }

    public string ReturnType()
    {
        return this._collectibleType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if(PlayerControls.instance.GetHealth()+_healthRestore> PlayerControls.instance.GetStartingHealth())
            {
                PlayerControls.instance.UpdateHealth(PlayerControls.instance.GetStartingHealth());
            }
            else
            {
                PlayerControls.instance.Heal(_healthRestore);
            }
            
            SoundSystem.instance.PlayHealSound();
            Collect();
        }
    }

}
