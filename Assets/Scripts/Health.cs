using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(IDieController))]
public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 10f;
    [SerializeField] private LayerMask DamagingLayers;
    [SerializeField] private Image HealthBar;
    [SerializeField] private TextMeshProUGUI HealthText;

    private float _currentHealth;

    void Awake()
    {
        _currentHealth = MaxHealth;
        UpdateUI();
    }

    void TakeDamage(DamageSource damageSource)
    {
        _currentHealth = Mathf.Max(_currentHealth - damageSource.DamageAmount, 0);
        UpdateUI();

        if (_currentHealth == 0 && TryGetComponent(out IDieController dieController))
        {
            dieController.Die();
        }
    }

    void UpdateUI()
    {
        if (!HealthBar) return;
        HealthBar.fillAmount = _currentHealth / MaxHealth;
        if (!HealthText) return;
        HealthText.text = $"{_currentHealth}/{MaxHealth}";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((DamagingLayers & 1 << col.gameObject.layer) == 0)
        {
            return;
        }

        if (col.gameObject.TryGetComponent(out DamageSource damageSource))
        {
            TakeDamage(damageSource);
        }

    }
}