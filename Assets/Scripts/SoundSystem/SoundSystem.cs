using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] AudioSource _bgmPiano;
    [SerializeField] AudioSource _bgmSax;
    [SerializeField] AudioSource _bgmGuitar;
    [SerializeField] AudioSource _characterSwitch;
    [SerializeField] AudioSource _guitarAbility;
    [SerializeField] AudioSource _pianoAbility;
    [SerializeField] AudioSource _saxAbility;
    [SerializeField] AudioSource _setCheckpoint;
    [SerializeField] AudioSource[] _recordSounds = new AudioSource[6];

    private string _saxType = "sax";
    private string _pianoType = "piano";
    private string _guitarType = "guitar";
    private Dictionary<string, AudioSource> characterAudioDict = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> characterBGMDict = new Dictionary<string, AudioSource>();

    public static SoundSystem instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void onStartAddType()
    {
        characterAudioDict.Add(this._saxType, this._saxAbility);
        characterAudioDict.Add(this._pianoType, this._pianoAbility);
        characterAudioDict.Add(this._guitarType, this._guitarAbility);

        characterBGMDict.Add(this._saxType, this._bgmSax);
        characterBGMDict.Add(this._pianoType, this._bgmPiano);
        characterBGMDict.Add(this._guitarType, this._bgmGuitar);
    }
    public void PlayBGMOnStart()
    {
        if (characterBGMDict.ContainsKey(PlayerControls.instance.GetCurrentCharacterType()))
        {
            AudioSource currentTypeBGM = characterBGMDict[PlayerControls.instance.GetCurrentCharacterType()];
            currentTypeBGM.Play();
        }
    }

    public void PlayRecordCutsceneMusic(int index)
    {
        this.stopCurrentBGM();

        this._recordSounds[index].Play();
    }

    public void StopRecordCutsceneMusic(int index)
    {
        this._recordSounds[index].Stop();
        this.PlayBGMOnStart();
    }
    public void PlaySetCheckpoint()
    {
        this._setCheckpoint.Play();
    }
    public void playCharacterSwitch()
    {
        this._characterSwitch.Play();
    }
    public void playNewBGM()
    {
        this.playCharacterSwitch();

        if (characterBGMDict.ContainsKey(PlayerControls.instance.GetCurrentCharacterType()))
        {
            AudioSource currentTypeBGM = characterBGMDict[PlayerControls.instance.GetCurrentCharacterType()];
            currentTypeBGM.Play();
        }
    }
    public void stopCurrentBGM()
    {
        if (characterBGMDict.ContainsKey(PlayerControls.instance.GetCurrentCharacterType()))
        {
            AudioSource currentTypeAudio = characterBGMDict[PlayerControls.instance.GetCurrentCharacterType()];
            currentTypeAudio.Stop();
        }
    }
    public void playByPlayerType()
    {
        // this function is called from the player controls script
        if (characterAudioDict.ContainsKey(PlayerControls.instance.GetCurrentCharacterType()))
        {
            AudioSource currentAbilityAudio = characterAudioDict[PlayerControls.instance.GetCurrentCharacterType()];
            currentAbilityAudio.Play();
        }

    }

}
