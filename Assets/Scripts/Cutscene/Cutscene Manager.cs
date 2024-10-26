using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] _recordPlayerPrefab = new GameObject[6];
    [SerializeField] ParticleSystem _playerEffect;
    [SerializeField] GameObject _recordAnim;
    [SerializeField] Image _vingette;
    private Dictionary<string, string[]> _scenesText = new Dictionary<string, string[]>();
    private bool[] _hasRecordsPlayed = new bool[6];
    private string[] _recordTitles = new string[6];
    private bool _hasSwitchDisplayed = false;
    private bool _hasSaxDisplayed = false;
    private bool _hasPianoDisplayed = false;
    private bool _hasGuitarDisplayed = false;
    private bool _hasTutorialDisplayed = false;
    private string _tutorialKey = "tutorial";
    private string _firstRecordKey = "firstRecord";
    private string _secondRecordKey = "secondRecord";
    private string _thirdRecordKey = "thirdRecord";
    private string _fourthRecordKey = "fourthRecord";
    private string _fifthRecordKey = "fifthRecord";
    private string _sixthRecordKey = "sixRecord";
    private string _saxKey = "sax";
    private string _pianoKey = "piano";
    private string _guitarKey = "guitar";
    private string _jukeboxKey = "jukebox";
    private string _recordsCollectedKey = "collected";
    private string _jukeboxFullKey = "jukeboxComplete";
    private string _firstEncounterKey = "firstEncounter";
    private string _typeRecord = "record";
    private string _typeOther = "other";
    private float _recordFadeDuration = 0.1f; 

    public static CutsceneManager instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void StartCutscene(string key, bool disableControls, string type)
    {

        
        DisplayText.instance.DisplayMessagesSequentially(_scenesText[key], disableControls, type);


    }

    public void StartRecordCutscene()
    {
        int index = 0;
        foreach(bool hasPlayed in this._hasRecordsPlayed)
        {
            if (hasPlayed == false)
            {
                StartCutscene(this._recordTitles[index], true, _typeRecord);
                break;
            }
            else
            {
                index++;
            }
        }
        
    }

    public void DisableVingette()
    {
        this._vingette.enabled = false;
    }
    public void EnableVingette()
    {
        this._vingette.enabled = true;
    }
    private void FadeInRecordAnim()
    {
        StartCoroutine(FadeSprite(0, 1));
    }

    private void FadeOutRecordAnim()
    {
        StartCoroutine(FadeSprite(1, 0));
    }
    public void EnableRecordShowing()
    {
        this._recordAnim.transform.position = PlayerControls.instance.GetPlayerTransform();
        FadeInRecordAnim();
        this._recordAnim.SetActive(true);
    }

    private IEnumerator FadeSprite(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = _recordAnim.GetComponent<SpriteRenderer>().color;

        while (elapsedTime < _recordFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / _recordFadeDuration);
            _recordAnim.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        _recordAnim.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, endAlpha);
    }

    public void DisableRecordShowing()
    {
        FadeOutRecordAnim();
        this._recordAnim.SetActive(false);
    }

    private void ChangeRecordPlayedTrue(int index)
    {
        this._hasRecordsPlayed[index] = true;
    }
    public void PlayRecordCutsceneSound()
    {
        int index = 0;
        foreach (bool hasPlayed in this._hasRecordsPlayed)
        {
            if (hasPlayed == false)
            {
                SoundSystem.instance.PlayRecordCutsceneMusic(index);
                break;
            }
            else
            {
                index++;
            }
        }
    }

    public void StopRecordCutscene()
    {
        StopRecordCutsceneSound();

        int index = 0;
        foreach (bool hasPlayed in this._hasRecordsPlayed)
        {
            if (hasPlayed == false)
            {
                ChangeRecordPlayedTrue(index);
                break;
            }
            else
            {
                index++;
            }
        }
    }
    public void StopRecordCutsceneSound()
    {
        int index = 0;
        foreach (bool hasPlayed in this._hasRecordsPlayed)
        {
            if (hasPlayed == false)
            {
                SoundSystem.instance.StopRecordCutsceneMusic(index);
                break;
            }
            else
            {
                index++;
            }
        }
    }

    public void StartFirstJukeCutscene()
    {
        StartCutscene(_jukeboxKey, true, _typeOther);
    }

    public void StartFinalCutscene()
    {
        StartCutscene(_jukeboxFullKey, true, _typeOther);
    }
    public void StartRecordCollectedScene()
    {
        StartCutscene(_recordsCollectedKey, true, _typeOther);
    }
    public void SetRecordArraysAtStart()
    {
        for(int index=0; index < this._hasRecordsPlayed.Length; index++)
        {
            this._hasRecordsPlayed[index] = false;
        }

        this._recordTitles[0] = this._firstRecordKey;
        this._recordTitles[1] = this._secondRecordKey;
        this._recordTitles[2] = this._thirdRecordKey;
        this._recordTitles[3] = this._fourthRecordKey;
        this._recordTitles[4] = this._fifthRecordKey;
        this._recordTitles[5] = this._sixthRecordKey;
    }

    public void SetHasTutorialPlayedTrue()
    {
        this._hasTutorialDisplayed = true;
    }

    public bool GetHasTutorialPlayed()
    {
        return this._hasTutorialDisplayed;
    }

    public void StartSaxCutscene()
    {
        SetSaxHasPlayedTrue();
        StartCutscene(this._saxKey, false, _typeOther);
    }

    public void StartPianoCutscene()
    {
        SetPianoHasPlayedTrue();
        StartCutscene(this._pianoKey, false, _typeOther);
    }

    public void StartGuitarCutscene()
    {
        SetGuitarHasPlayedTrue();
        StartCutscene(this._guitarKey, false, _typeOther);
    }
    public void StartTutorialCutscene()
    {
        DisableVingette();
        StartCutscene(this._tutorialKey, true, _typeOther);
        SetHasTutorialPlayedTrue();
    }

    public void StartFirstEncounterCutscene()
    {
        StartCutscene(this._firstEncounterKey, true, _typeOther);
    }
    public void SetSaxHasPlayedTrue()
    {
        this._hasSaxDisplayed = true;
    }

    public void SetPianoHasPlayedTrue()
    {
        this._hasPianoDisplayed = true;
    }

    public void SetGuitarHasPlayedTrue()
    {
        this._hasGuitarDisplayed = true;
    }

    public void SetSwitchHasPlayedTrue()
    {
        this._hasSwitchDisplayed = true;
    }

    public bool GetHasGuitarPlayed()
    {
        return this._hasGuitarDisplayed;
    }

    public bool GetHasPianoPlayed()
    {
        return this._hasPianoDisplayed;
    }
    public bool GetHasSaxPlayed()
    {
        return this._hasSaxDisplayed;
    }

    public bool GetHasSwitchPlayed()
    {
        return this._hasSwitchDisplayed;
    }

    public void EnableParticleSystem()
    {
        if(_playerEffect!=null && !_playerEffect.isPlaying)
        {
            _playerEffect.transform.position = PlayerControls.instance.GetPlayerTransform();
            _playerEffect.Play();
        }
    }

    public void DisableParticleSystem()
    {
        if (_playerEffect != null && _playerEffect.isPlaying)
        {
            _playerEffect.Stop();
        }
    }

    public void SetScenesContent()
    {
        _scenesText[_tutorialKey] = new string[] { "Where am I...? This feels... familiar, but different...", "Oh, and my instrument is with me... I wonder how it sounds here.", "*To play your instrument, press the O button*" };
        _scenesText[_firstRecordKey] = new string[] { "Oh, this is the most popular song from my parents' band.\n I could watch their performance videos forever...", "Dad, I think I understand.","I need to collect the records\nwith music that will awaken Mom's memories of music!" };
        _scenesText[_secondRecordKey] = new string[] { "This melody...\nMom used to sing it to me when I was little, right before bed.\nI wish I could go back to those times when Dad was still with us..." };
        _scenesText[_thirdRecordKey] = new string[] { "This is the song they danced to at their wedding... I remember Mom telling me about it with such warmth." };
        _scenesText[_fourthRecordKey] = new string[] { "Dad always played this solo with such passion... Mom said that in those moments, it was like he became one with his music." };
        _scenesText[_fifthRecordKey] = new string[] { "Mom was so proud of this performance... I've heard this story a hundred times, but every time I saw the same sense of triumph in her eyes." };
        _scenesText[_sixthRecordKey] = new string[] { "They always sang this song together...\n But after Dad passed, Mom couldn’t bring herself to play it anymore.\n She just stood in front of the piano and cried... She thought I didn’t notice." };
        _scenesText[_guitarKey] = new string[] { "Ooh, the sound coming from the guitar is so powerful,\n it feels like it could tear down an entire building!", "*To switch instruments, press the P button*" };
        _scenesText[_pianoKey] = new string[] { "When I play the piano, it's as if some invisible energy surrounds me.\n I feel protected in it.", "*To switch instruments, press the P button*" };
        _scenesText[_saxKey] = new string[] { "It seems when I play the saxophone, I become much lighter!\n Like gravity doesn't affect me as much anymore!", "*To switch instruments, press the P button*" };
        _scenesText[_jukeboxKey] = new string[] { "A door? Maybe I can exit through there?", "…", "But it's locked...", "What's this old jukebox doing here?\nMaybe I need to insert the ones I've found?", "Hmm, looks like it's missing six records.\nI’ll go look for the others!" };
        _scenesText[_recordsCollectedKey] = new string[] { "Finally, I’ve collected all the records! I need to go back to the jukebox!" };
        _scenesText[_jukeboxFullKey] = new string[] { "Now let’s put them all here...", "Thank you, Dad, now I know how to make her remember again! We miss you..." };
        _scenesText[_firstEncounterKey] = new string[] { "Hmm.. Something doesn't feel right...", "I should be careful." };
    }

    
}
