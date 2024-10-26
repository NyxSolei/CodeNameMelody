using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
 
    public void GoToGame()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene("SampleScene");
    }
}
