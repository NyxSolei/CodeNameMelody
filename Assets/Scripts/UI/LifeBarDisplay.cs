using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LifeBarDisplay : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] Sprite _regularFill;
    [SerializeField] Sprite _onHealFill;
    public Image _fillImage;

    public static LifeBarDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (_healthSlider != null)
        {
            _fillImage = _healthSlider.fillRect.GetComponent<Image>();
            _fillImage.sprite = _regularFill;
        }
    }
    public void UpdateHealthBar()
    {
        this._healthSlider.value = PlayerControls.instance.GetHealth();
    }

    public void UpdateHealthBarOnHeal()
    {
        StartCoroutine(ChangeFillSpriteOnHeal());
        this._healthSlider.value = PlayerControls.instance.GetHealth();
    }
    public void InitializeHealthBar()
    {
        this._healthSlider.value = PlayerControls.instance.GetStartingHealth();
        this.SetMaxHealthBarDisplay();
    }

    public void SetMaxHealthBarDisplay()
    {
        this._healthSlider.maxValue = PlayerControls.instance.GetStartingHealth();
    }

    private IEnumerator ChangeFillSpriteOnHeal()
    {
        _fillImage.sprite = _onHealFill;

        Canvas.ForceUpdateCanvases();

        yield return new WaitForSeconds(2);

        _fillImage.sprite = _regularFill;
    }
}
