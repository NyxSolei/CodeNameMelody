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

    public void IncreaseRestartCount()
    {
        _restartCount+=1;
    }
    public void updateRestartCount()
    {
        for(int index=0; index<3; index++)
        {
            if (_restartImages[index].enabled)
            {
                _restartImages[index].enabled = false;
                return;
            }
        }
    }

    public int GetRestartCount()
    {
        int restartCount = 0;
        for (int index = 0; index < 3; index++)
        {
            if (!_restartImages[index].enabled)
            {
                restartCount++;
            }
        }

        return restartCount;
    }

}
