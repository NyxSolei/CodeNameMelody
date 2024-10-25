using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text _displayedText;
    private float _timePerCharacter = 0.05f;
    private float fadeDuration = 1.0f;
    private Coroutine hideTextCoroutine;
    private string _typeRecord = "record";

    public static DisplayText instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void TextDisplay(string textoToDisplay)
    {
        this._displayedText.text = textoToDisplay;
        this._displayedText.enabled = true;

        float displayTime = textoToDisplay.Length * _timePerCharacter;

        if (hideTextCoroutine != null)
        {
            StopCoroutine(hideTextCoroutine);
        }

        hideTextCoroutine = StartCoroutine(ShowTextWithFade(displayTime));
    }

    private IEnumerator ShowTextWithFade(float displayTime)
    {
        yield return StartCoroutine(FadeTextIn());
        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeTextOut());

        this._displayedText.enabled = false;
    }

    private IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        Color textColor = this._displayedText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            this._displayedText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;
        Color textColor = this._displayedText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            this._displayedText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }

    public void DisplayMessagesSequentially(string[] texts, bool disableControls, string type)
    {
        if (disableControls)
        {
            PlayerControls.instance.SetDisableControls();
            CutsceneManager.instance.EnableParticleSystem();

            if(type == _typeRecord)
            {
                CutsceneManager.instance.PlayRecordCutsceneSound();
            }
        }
        StartCoroutine(DisplayMessagesRoutine(texts, disableControls, type));



    }

    private IEnumerator DisplayMessagesRoutine(string[] texts, bool disableControls, string type)
    {
        foreach (string message in texts)
        {
            TextDisplay(message);

            
            float displayTime = message.Length * _timePerCharacter + 2 * fadeDuration; 

            yield return new WaitForSeconds(displayTime);
        }

        if (disableControls)
        {
            CutsceneManager.instance.DisableParticleSystem();
            PlayerControls.instance.EnableControls();

            if (type == _typeRecord)
            {
                CutsceneManager.instance.StopRecordCutscene();
            }
        }
    }
}
