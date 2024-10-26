using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPoint : MonoBehaviour
{
    private string _playerTag = "Player";
    private bool _isSet = false;
    public ParticleSystem _respaunPointEffect;
    [SerializeField] Text _checkpointReached;
    private float _timePerCharacter = 0.05f;
    private float fadeDuration = 1.0f;
    private Coroutine hideTextCoroutine;
    private string _textContent = "Checkpoint saved!";



    private void Start()
    {
        _respaunPointEffect.Stop();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(this._playerTag) && !this._isSet)
        {
            PlayerControls.instance.SetCheckpoint(this.transform.position.x, this.transform.position.y);
            SoundSystem.instance.PlaySetCheckpoint();
            this._isSet = !this._isSet;
            TextDisplay(this._textContent);

            _respaunPointEffect.Play();
        }
    }

    public void TextDisplay(string textoToDisplay)
    {
        this._checkpointReached.text = textoToDisplay;
        this._checkpointReached.enabled = true;

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

        this._checkpointReached.enabled = false;
    }

    private IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        Color textColor = this._checkpointReached.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            this._checkpointReached.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;
        Color textColor = this._checkpointReached.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            this._checkpointReached.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }
}
