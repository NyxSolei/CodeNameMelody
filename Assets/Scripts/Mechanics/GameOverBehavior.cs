using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehavior : MonoBehaviour
{

    private int _restartAllowance = 3;

    public static GameOverBehavior instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool GameOverCheck()
    {
        if(RestartDisplay.instance.GetRestartCount() == _restartAllowance)
        {
            return true;
        }

        return false;
    }

    public void GameOverScreenDisplay()
    {
        SceneManager.LoadScene("BadEnding");
    }
}
