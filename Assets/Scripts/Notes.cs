using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour, IItem
{
    private string _collectibleType = "Note";
    public void Collect()
    {
        Destroy(gameObject);
    }

    public string ReturnType()
    {
        return this._collectibleType;
    }
}

