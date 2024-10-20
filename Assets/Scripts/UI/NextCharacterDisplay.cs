using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextCharacterDisplay : MonoBehaviour
{
    [SerializeField] Sprite _guitarDisplay;
    [SerializeField] Sprite _saxDisplay;
    [SerializeField] Sprite _pianoDisplay;
    private string _saxType = "sax";
    private string _pianoType = "piano";
    private string _guitarType = "guitar";
    private bool _elementsAreSet = false;

    private Dictionary<string, Sprite> _displayRotation = new Dictionary<string, Sprite> { };


    public static NextCharacterDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetSpriteOnStart()
    {
        if (this._elementsAreSet)
        {
            this.GetComponent<Image>().sprite = this._displayRotation[PlayerControls.instance.GetNextCharacterType()];
        }
        
    }

    public void ChangeToNextSprite()
    {
        this.GetComponent<Image>().sprite = this._displayRotation[PlayerControls.instance.GetNextCharacterType()];
    }

    public void AddDictionaryElementsOnStart()
    {
        this._displayRotation.Add(this._saxType, this._saxDisplay);
        this._displayRotation.Add(this._pianoType, this._pianoDisplay);
        this._displayRotation.Add(this._guitarType, this._guitarDisplay);
        this._elementsAreSet = true;
    }
}
