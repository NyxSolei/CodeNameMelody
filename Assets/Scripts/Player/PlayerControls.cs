using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControls : MonoBehaviour, DamageInterface.IDamagable
{
    [SerializeField] Sprite GuitarPlayerSprite;
    [SerializeField] Sprite PianoPlayerSprite;
    [SerializeField] Sprite SaxPlayerSprite;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] ParticleSystem _shieldParticle;
    [SerializeField] ParticleSystem _outerShieldParticle;
    [SerializeField] ParticleSystem _onChangeParticles;

    //privates
    private Rigidbody2D _rb;
    private Animator _animator;
    private string _guitarType = "guitar";
    private string _saxType = "sax";
    private string _pianoType = "piano";
    private string[] _characterType = new string[3];
    private int _firstCharacterTypeIndex = 0;
    private int _secondCharacterTypeIndex = 1;
    private int _thirdCharacterTypeIndex = 2;
    private int _currentCharacterType = 0;
    private int _increaseIncrement = 1;
    private int _maxCharacterTypeIndex = 2;
    private Sprite _currentSprite;
    private int _randomIndex;
    private KeyCode _characterChangeTriggerKey = KeyCode.P;
    private KeyCode _abilityUsageTriggerKey = KeyCode.O;
    private KeyCode _jumpKey = KeyCode.Space;
    private float _currentJumpForce;
    private bool _hasFlipped = false;
    private int _localScaleFlipIncrement = -1;
    private int _moveDirectionBase = 0;
    private int _startingHealthPoints = 40;
    private float _groundCheckRadius = 0.3f;
    private bool _isGrounded;
    private float _startXLocation;
    private float _startYLocation;
    private int _minHealth = 0;
    private float _lastCheckpointX;
    private float _lastCheckpointY;
    private float _moveSpeed = 10f;
    private float _jumpForce = 25f;
    private float _saxJumpForce = 30f;
    private Dictionary<string, int> _collectibleInventory = new Dictionary<string, int>{ { "MusicRecord", 0 }, { "Note", 0 } };
    private int collectibleIncrement = 1;
    private string _playerTag = "Player";
    private int _deathYtransform = -10;
    private string _speedAnimLabel = "Speed";
    private string _isPianoAnimLabel = "IsPiano";
    private string _isSaxAnimLabel = "IsSax";
    private string _isGuitarAnimLabel = "IsGuitar";
    private string _isJumpingAnimLabel = "IsJumping";
    private string _healthAnimLabel = "Health";
    private float _damageTakenXPushback = -10;
    private bool _disableControls = false;
    private int _shieldPower = 0;
    private bool _firstTimeChange = true;

    public static PlayerControls instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public int GetShieldPower()
    {
        return _shieldPower;
    }

    public void SetShieldPower(int power)
    {
        this._shieldPower = power;
    }
    public void ChangeCurrentCharacterSprite()
    {
        
        if (this.GetCurrentCharacterType() == this._guitarType)
        {
            //this._currentSprite = this.GuitarPlayerSprite;
            this._animator.SetBool(this._isGuitarAnimLabel, true);
            this._animator.SetBool(this._isPianoAnimLabel, false);
            this._animator.SetBool(this._isSaxAnimLabel, false);
            //handle shield removal
            PlayerControlPiano.instance.RemoveShield();
        }
        else if(this.GetCurrentCharacterType() == this._saxType)
        {
            //this._currentSprite = this.SaxPlayerSprite;
            this._animator.SetBool(this._isGuitarAnimLabel, false);
            this._animator.SetBool(this._isPianoAnimLabel, false);
            this._animator.SetBool(this._isSaxAnimLabel, true);
            // handle shield removal
            PlayerControlPiano.instance.RemoveShield();
        }
        else
        {
            //this._currentSprite = this.PianoPlayerSprite;
            this._animator.SetBool(this._isGuitarAnimLabel, false);
            this._animator.SetBool(this._isPianoAnimLabel, true);
            this._animator.SetBool(this._isSaxAnimLabel, false);
        }

        //this.GetComponent<SpriteRenderer>().sprite = this._currentSprite;
    }
    public void SetStartingCharacter()
    {
        // randomize choice
        this._randomIndex = Random.Range(this._firstCharacterTypeIndex, _characterType.Length);

        // set current character type
        this.SetCurrentCharacterType(this._randomIndex);
    }
    public void SetCurrentCharacterType(int index)
    {
        this._currentCharacterType = index;
    }
    public string GetCurrentCharacterType()
    {
        return this._characterType[this._currentCharacterType];
    }
    public int GetCurrentCharacterTypeIndex()
    {
        return this._currentCharacterType;
    }
    private void SetCharacterTypes()
    {
        this._characterType[this._firstCharacterTypeIndex] = _guitarType;
        this._characterType[this._secondCharacterTypeIndex] = _saxType;
        this._characterType[this._thirdCharacterTypeIndex] = _pianoType;
    }
    private int GetMaxCharacterTypeIndex()
    {
        return this._maxCharacterTypeIndex;
    }
    private int GetIncreaseIncrement()
    {
        return this._increaseIncrement;
    }

    public string GetNextCharacterType()
    {
        if (this.GetCurrentCharacterTypeIndex() == this.GetMaxCharacterTypeIndex())
        {
            return this._characterType[this._firstCharacterTypeIndex];
        }

        return this._characterType[this.GetCurrentCharacterTypeIndex() + this.GetIncreaseIncrement()];
    }
    private void TriggerCharacterChange()
    {
        // increases character index by 1
        // if index out of bounds, returns index to 0
        SoundSystem.instance.stopCurrentBGM();
        if (!_firstTimeChange)
        {
            _onChangeParticles.Play();
        }
        
        if (this.GetCurrentCharacterTypeIndex() == this.GetMaxCharacterTypeIndex())
        {
            this.SetCurrentCharacterType(this._firstCharacterTypeIndex);
        }
        else
        {
            this.SetCurrentCharacterType(this.GetCurrentCharacterTypeIndex() + this.GetIncreaseIncrement());
        }

        // changes the sprite
        this.ChangeCurrentCharacterSprite();
        //new bgm play
        SoundSystem.instance.playNewBGM();
        // display
        NextCharacterDisplay.instance.ChangeToNextSprite();
    }
    private void CheckButtonToTriggerChange()
    {
        if (Input.GetKeyDown(_characterChangeTriggerKey))
        {
            this.TriggerCharacterChange();
        }
    }

    public void DisplayShieldOnCharacter()
    {
        _shieldParticle.Play();
        _outerShieldParticle.Play();
    }

    public void StopShieldOnCharacter()
    {
        _shieldParticle.Stop();
        _outerShieldParticle.Stop();
    }
    private void CheckButtonToTriggerAbility()
    {
        if (Input.GetKeyDown(_abilityUsageTriggerKey))
        {
            SoundSystem.instance.playByPlayerType();
            if (this.GetCurrentCharacterType() == this._guitarType)
            {
                PlayerControlGuitar.instance.UseAbility();
                if (!CutsceneManager.instance.GetHasGuitarPlayed())
                {
                    CutsceneManager.instance.StartGuitarCutscene();
                }
            }
            else if (this.GetCurrentCharacterType() == this._saxType)
            {
                PlayerControlSax.instance.UseAbility();
                this.Jump();
                if (!CutsceneManager.instance.GetHasSaxPlayed())
                {
                    CutsceneManager.instance.StartSaxCutscene();
                }
            }
            else if (PlayerControlPiano.instance.CooldownComplete() || PlayerControlPiano.instance.GetIsShieldSet())
            {


                PlayerControlPiano.instance.UseAbility();
                if (!CutsceneManager.instance.GetHasPianoPlayed())
                {
                    CutsceneManager.instance.StartPianoCutscene();
                }
            }
        }
    }
    public void Jump()
    {
        if ((Input.GetKeyDown(this._jumpKey)|| (Input.GetKeyDown(this._abilityUsageTriggerKey) && (this.GetCurrentCharacterType() == this._saxType)) )&& this.GetIsGrounded())
        {


            if (this.GetCurrentCharacterType() == this._saxType)
            {
                
                this.SetPlayerCurrentJumpForce(PlayerControlSax.instance.GetJumpForce());
            }
            else
            {
                
                // to complete
                this.SetPlayerCurrentJumpForce(PlayerControlGuitar.instance.GetJumpForce());
            }

            // animator handling
            this._animator.SetBool(this._isJumpingAnimLabel, true);
            this._rb.velocity = new Vector2(this._rb.velocity.x, this._currentJumpForce);
        }
        else if(!Input.GetKeyDown(this._jumpKey) && this.GetIsGrounded())
        {
            this._animator.SetBool(this._isJumpingAnimLabel, false);
        }

    }
    public void SetPlayerCurrentJumpForce(float jumpForce)
    {
        this._currentJumpForce = jumpForce;
    }
    public void Move()
    {
        
        float _moveDirection = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(_moveDirection * PlayerControlGuitar.instance.GetMoveSpeed(), this._rb.velocity.y);
        this._rb.velocity = movement;

        // flip according to movement
        if((_moveDirection < this._moveDirectionBase && !this.GetHasFlipped()) || (_moveDirection > this._moveDirectionBase && this.GetHasFlipped()))
        {
            this.FlipSprite();
        }
        // check that it is not jumping
        if (this.GetIsGrounded())
        {
            this._animator.SetBool(this._isJumpingAnimLabel, false);
            this._animator.SetFloat(this._speedAnimLabel, Mathf.Abs(_moveDirection));
        }
        else
        {
            this._animator.SetFloat(this._speedAnimLabel, 0);
            this._animator.SetBool(this._isJumpingAnimLabel, true);
        }
    }
    public bool GetHasFlipped()
    {
        return this._hasFlipped;
    }
    public void FlipHasFlipped()
    {
        this._hasFlipped = !this._hasFlipped;
    }
    public void FlipSprite()
    {
        Vector2 curremtScale = gameObject.transform.localScale;
        curremtScale.x *= this._localScaleFlipIncrement;
        gameObject.transform.localScale = curremtScale;
        this.FlipHasFlipped();
    }
    public void SetCharactersJump()
    {
        PlayerControlGuitar.instance.SetJumpForce(this._jumpForce);
        PlayerControlPiano.instance.SetJumpForce(this._jumpForce);
        PlayerControlSax.instance.SetJumpForce(this._saxJumpForce);
    }
    public void SetCharactersSpeed()
    {
        PlayerControlGuitar.instance.SetMoveSpeed(this._moveSpeed);
        PlayerControlPiano.instance.SetMoveSpeed(this._moveSpeed);
        PlayerControlSax.instance.SetMoveSpeed(this._moveSpeed);
    }
    public void SetPlayerCharacterAtStart()
    {
        StopShieldOnCharacter();
        // get rigidbody
        this._rb = GetComponent<Rigidbody2D>();
        // get animator
        this._animator = GetComponent<Animator>();
        // set character types
        this.SetCharacterTypes();
        this.SetStartingCharacter();
        this.SetCharactersSpeed();
        this.SetCharactersJump();
        this.ChangeCurrentCharacterSprite();
        this.UpdateHealthAtStart();
        this.SetDefaultRespawnPoint();
        // set player tag
        this.gameObject.tag = this._playerTag;
        //handling sound
        SoundSystem.instance.onStartAddType();
        SoundSystem.instance.PlayBGMOnStart();
        // display
        NextCharacterDisplay.instance.AddDictionaryElementsOnStart();
        NextCharacterDisplay.instance.SetSpriteOnStart();
        LifeBarDisplay.instance.InitializeHealthBar();
        // handle projectile load
        ProjectileManager.instance.LoadAllSprites();
        // handle text loading
        CutsceneManager.instance.SetScenesContent();
        CutsceneManager.instance.SetRecordArraysAtStart();
        // handle particle sys
        CutsceneManager.instance.DisableParticleSystem();
        // handle record anim
        CutsceneManager.instance.DisableRecordShowing();
        // finish first char change
        _firstTimeChange = false;



    }
    public void UpdateHealthAtStart()
    {
        PlayerControlGuitar.instance.SetHealth(this._startingHealthPoints);
        PlayerControlPiano.instance.SetHealth(this._startingHealthPoints);
        PlayerControlSax.instance.SetHealth(this._startingHealthPoints);
        // hanlde life bar
        LifeBarDisplay.instance.InitializeHealthBar();
        // handle anim
        this._animator.SetInteger(this._healthAnimLabel, this._startingHealthPoints);
    } 
    public void UpdateHealth(int hp)
    {
        int updateHp = this.GetHealth() + hp;
        PlayerControlGuitar.instance.SetHealth(updateHp);
        PlayerControlPiano.instance.SetHealth(updateHp);
        PlayerControlSax.instance.SetHealth(updateHp);
        // hanlde life bar
        LifeBarDisplay.instance.UpdateHealthBar();
        // handle anim
        this._animator.SetInteger(this._healthAnimLabel, updateHp);
    }

    public void Heal(int hp)
    {
        int updateHp = this.GetHealth() + hp;
        PlayerControlGuitar.instance.SetHealth(updateHp);
        PlayerControlPiano.instance.SetHealth(updateHp);
        PlayerControlSax.instance.SetHealth(updateHp);
        // hanlde life bar
        LifeBarDisplay.instance.UpdateHealthBarOnHeal();
        this._animator.SetInteger(this._healthAnimLabel, updateHp);
    }

    public int GetStartingHealth()
    {
        return this._startingHealthPoints;
    }
    public void TakeDamage(int damageTaken)
    {
        //update damage
        PlayerControlGuitar.instance.TakeDamage(damageTaken);

        PlayerControlPiano.instance.TakeDamageWithShield(damageTaken);

        PlayerControlSax.instance.TakeDamage(damageTaken);

        //move with damage taken
        this.PlayerHurtMovemet();

        //handle sound
        SoundSystem.instance.PlayHitSound();

        // handle display
        LifeBarDisplay.instance.UpdateHealthBar();

        // handle anim

        this._animator.SetInteger(this._healthAnimLabel, this.GetHealth());
    }

    public int GetHealth()
    {
        if(PlayerControlPiano.instance.GetHealth() == PlayerControlGuitar.instance.GetHealth())
        {
            return PlayerControlGuitar.instance.GetHealth();
        }
        else
        {
            return PlayerControlPiano.instance.GetHealth();
        }
    }
    private bool GetIsGrounded()
    {
        return this._isGrounded;
    }
    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
    private void PlayerHurtMovemet()
    {
        this._rb.velocity = new Vector2(this._rb.velocity.x+this._damageTakenXPushback, PlayerControlGuitar.instance.GetJumpForce());
    }
    public int GetMinHealth()
    {
        return this._minHealth;
    }
    public void DeathCheck()
    {
        // add check to below min platform y position
        if(PlayerControlGuitar.instance.GetHealth()<=this.GetMinHealth() || PlayerControlPiano.instance.GetHealth()<=this.GetMinHealth() || PlayerControlSax.instance.GetHealth() <= this.GetMinHealth())
        {
            // add here the death scene music and stuff
            this.Die();
            return;
        }
        else if (this.gameObject.transform.position.y <= this._deathYtransform)
        {
            this.Die();
        }
    }
    public void SetCheckpoint(float checkpointX, float checkpointY)
    {
        this._lastCheckpointX = checkpointX;
        this._lastCheckpointY = checkpointY;
    }
    public Vector2 GetLastCheckpoint()
    {
        return new Vector2(this._lastCheckpointX, this._lastCheckpointY);
    }
    public void SetDefaultRespawnPoint()
    {
        SetCheckpoint(this._rb.position.x, this._rb.position.y);
    }

    private IEnumerator DeathSequence()
    {
        _animator.SetTrigger("Death");
        SoundSystem.instance.PlayDeathSound();
        SetDisableControls();
        yield return new WaitForSeconds(3);

        EnableControls();
        _animator.ResetTrigger("Death");
        this._rb.position = this.GetLastCheckpoint();
        RestartDisplay.instance.updateRestartCount();
        Heal(20);
    }

    public void Die()
    {
        if (!GameOverBehavior.instance.GameOverCheck())
        {
            StartCoroutine(DeathSequence());
        }
        else
        {
            GameOverBehavior.instance.GameOverScreenDisplay();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.gameObject.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            IncreaseCollectibleCount(item.ReturnType());
        }
    }

    private void IncreaseCollectibleCount(string collectibleType)
    {
        if (_collectibleInventory.ContainsKey(collectibleType))
        {
            
            _collectibleInventory[collectibleType] += this.collectibleIncrement;
            // UI handling
            CollectibleCountDisplay.instance.IncreaseCollectibleDisplay(collectibleType);

            if(collectibleType == "MusicRecord")
            {
                CutsceneManager.instance.StartRecordCutscene();
            }
            else
            {
                SoundSystem.instance.PlayCollectionSound();
            }

        }
    }

    public Dictionary<string, int> GetCollectibleInventory()
    {
        return this._collectibleInventory;
    }

    public void SetDisableControls()
    {
        this._disableControls = true;
    }

    public void EnableControls()
    {
        this._disableControls = false;
    }

    public bool GetDisableControls()
    {
        return this._disableControls;
    }

    private void DisableMovementAndResetState()
    {
        Vector2 movement = new Vector2(0, this._rb.velocity.y);
        this._animator.SetFloat(this._speedAnimLabel, 0f);
        this._rb.velocity = movement;
        this._animator.SetBool(this._isJumpingAnimLabel, false);
    }

    public Vector2 GetPlayerTransform()
    {
        return this.gameObject.transform.position;
    }

    private void Start()
    {
        this.SetPlayerCharacterAtStart();
    }

    private void Update()
    {
        if (!CutsceneManager.instance.GetHasTutorialPlayed())
        {
            // go to first cutscene
            CutsceneManager.instance.StartTutorialCutscene();
        }
        if (!this._disableControls)
        {
            this.Move();
            this.Jump();
            this.CheckButtonToTriggerAbility();
            this.CheckButtonToTriggerChange();
            
        }
        else
        {
            DisableMovementAndResetState();
        }

        this.GroundCheck();
        this.DeathCheck();

    }
}
