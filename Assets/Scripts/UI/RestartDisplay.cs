using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartDisplay : MonoBehaviour
{
    [SerializeField] Image[] _restartImages = new Image[3];
    private int _restartCount = 0;

    public static RestartDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void updateRestartCount()
    {
        _restartImages[_restartCount].enabled = false;
        _restartCount++;
    }

    public int GetRestartCount()
    {
        return this._restartCount;
    }

}
