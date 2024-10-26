using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueContent : MonoBehaviour
{
    private string[] _dialogueMessages = new string[] { "I did it… I got in.", "But Mom doesn’t understand…", "Mom used to love music,\nbut after you left us she forgot…", "Dad,\nhow can I make her remember again?" };


    public static DialogueContent instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void Start()
    {
        StartCoroutine(WaitAndExecute(3f));
        
    }

    private IEnumerator WaitAndExecute(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartSceneTextDisplay.instance.DisplayMessagesSequentially(this._dialogueMessages);
    }
}
