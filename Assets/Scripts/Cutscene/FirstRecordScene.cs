using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRecordScene : AbstractRecordScene
{
    private string _firstLine = "Oh, this is the most popular song from my parents' band. I could watch their performance videos forever...";
    private string _secondLine = "Dad, I think I understand. I need to collect the records with music that will awaken Mom's memories of music!";
    private int _arrayLength = 2;

    private static FirstRecordScene _instance;
    public static FirstRecordScene instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FirstRecordScene();
            }
            return _instance;
        }
    }
    public void InitializeScene()
    {
        SetContentArraySize(this._arrayLength);
        SetSceneContentInArray(this._firstLine, 1);
        SetSceneContentInArray(this._secondLine, 2);

    }
}
