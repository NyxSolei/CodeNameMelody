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
        if (CompareTag("Player")) 
        {
            PlayerControls.instance.UpdateHealth( _healthRestore); 

            Collect();
        }
    }

}
