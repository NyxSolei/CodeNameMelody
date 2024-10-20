using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LifeBarDisplay : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;

    public static LifeBarDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UpdateHealthBar()
    {
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
}
