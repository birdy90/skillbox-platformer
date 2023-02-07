using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 10f;
    [SerializeField] private LayerMask DamagingLayers;
    [SerializeField] private Image HealthBar;

    [SerializeField] private float _currentHealth;

    void Awake()
    {
        _currentHealth = MaxHealth;
    }

    void TakeDamage(DamageSource damageSource)
    {
        _currentHealth -= damageSource.DamageAmount;
        UpdateUI();

        if (_currentHealth <= 0)
        {
            Debug.Log("DIE");
        }
    }

    void UpdateUI()
    {
        HealthBar.fillAmount = _currentHealth / MaxHealth;
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