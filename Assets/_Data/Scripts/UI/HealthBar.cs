using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NhoxBehaviour
{
    [SerializeField] protected Damageable playerDamageable;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected Slider healthSLider;

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    protected override void Start()
    {
        base.Start();
        this.InitializeHealthBar();
    }
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerDamageable();
        this.LoadHealthSlider();
        this.LoadHealthText();
    }

    protected void LoadPlayerDamageable()
    {
        if (playerDamageable != null) return;
        playerDamageable = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Damageable>();
        Debug.Log(transform.name + " LoadPlayerDamageable", gameObject);
    }

    protected void LoadHealthText()
    {
        if (healthText != null) return;
        healthText = transform.Find("HealthTextBG").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadHealthText", gameObject);
    }

    protected void LoadHealthSlider()
    {
        if (healthSLider != null) return;
        healthSLider = GetComponentInChildren<Slider>();
        Debug.Log(transform.name + " LoadHealthSlider", gameObject);

    }

    protected void InitializeHealthBar()
    {
        healthSLider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthText.text = "HP: " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }

    protected float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    protected void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSLider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthText.text = "HP: " + newHealth + "/" + maxHealth;
    }
}
