using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningBehavior : MonoBehaviour
{
    private int _amountOfRecordToCollect = 6;
    private string _recordKey = "MusicRecord";

    public static WinningBehavior instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool WinConditionsCheck()
    {
        Dictionary<string, int> inventoryCheck = PlayerControls.instance.GetCollectibleInventory();

        if(inventoryCheck[_recordKey] == _amountOfRecordToCollect)
        {
            return true;
        }

        return false;
    }

}
