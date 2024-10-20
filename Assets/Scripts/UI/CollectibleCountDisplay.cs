using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollectibleCountDisplay : MonoBehaviour
{
    [SerializeField] Text CollectibleNoteDisplay;
    [SerializeField] Text CollectibleRecordDisplay;
    private Dictionary<string, Text> _collectibleDisplayInventory = new Dictionary<string, Text> {};
    private int _dictionaryDisplayUpdate = 0;
    private int _increaseIncrement = 1;
    private string _startingInv = "0";

    public static CollectibleCountDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        this.AddDictionaryElementsOnStart();
        this.ResetOnStart();
    }

    public void IncreaseCollectibleDisplay(string collectibleType)
    {
        if (this._collectibleDisplayInventory.ContainsKey(collectibleType))
        {
            int.TryParse(this._collectibleDisplayInventory[collectibleType].text.ToString(),out this._dictionaryDisplayUpdate);
            this._collectibleDisplayInventory[collectibleType].text = (this._dictionaryDisplayUpdate + this._increaseIncrement).ToString();
        }
    }

    private void ResetOnStart()
    {
        foreach(string displayKey in this._collectibleDisplayInventory.Keys)
        {
            this._collectibleDisplayInventory[displayKey].text = this._startingInv;
        }
    }

    private void AddDictionaryElementsOnStart()
    {
        this._collectibleDisplayInventory.Add("MusicRecord", this.CollectibleRecordDisplay);
        this._collectibleDisplayInventory.Add("Note", this.CollectibleNoteDisplay);
    }
}
