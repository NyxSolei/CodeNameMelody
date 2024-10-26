using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartSceneTextDisplay : MonoBehaviour
{
    public Text _displayedText;
    private float _timePerCharacter = 0.05f;
    private float fadeDuration = 1.0f;
    private Coroutine hideTextCoroutine;

    public static StartSceneTextDisplay instance;
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

    public void DisplayMessagesSequentially(string[] texts )
    {

        StartCoroutine(DisplayMessagesRoutine(texts));



    }

    private IEnumerator DisplayMessagesRoutine(string[] texts)
    {
        foreach (string message in texts)
        {
            TextDisplay(message);


            float displayTime = message.Length * _timePerCharacter + 2 * fadeDuration;

            yield return new WaitForSeconds(displayTime);
        }

        DialogueContent.instance.StartGame();
    }
}
