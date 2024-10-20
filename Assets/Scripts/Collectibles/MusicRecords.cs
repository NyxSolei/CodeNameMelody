using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRecords : MonoBehaviour, IItem
{
    private string _collectibleType = "MusicRecord";
    public void Collect()
    {
        Destroy(gameObject);
    }

    public string ReturnType()
    {
       return this._collectibleType;
    }
}
