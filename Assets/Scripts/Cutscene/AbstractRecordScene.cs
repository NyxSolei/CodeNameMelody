using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractRecordScene
{
    private string[] _sceneContentText;

    public void SetSceneContentInArray(string newContent, int index)
    {
        this._sceneContentText[index] = newContent;
    }
    public string[] GetSceneContent()
    {
        return this._sceneContentText;
    }

    public void SetContentArraySize(int size)
    {
        this._sceneContentText = new string[size];
    }
}
