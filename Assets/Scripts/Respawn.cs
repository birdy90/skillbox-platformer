using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Respawn : MonoBehaviour
{
    public Light2D LightSource;
    
    private bool _activated;
    private Animator _animator;
    private readonly float _activationDuration = 0.05f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Increase outer radius and change color
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_activated) return;
        
        _activated = true;
        _animator.SetTrigger(Constants.RespawnActivationTrigger);
        StartCoroutine(nameof(UpdateLightSource));
    }

    IEnumerator UpdateLightSource()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            Debug.Log(Time.time - startTime);
            LightSource.pointLightInnerRadius = Mathf.Lerp(LightSource.pointLightInnerRadius, 0f, _activationDuration);
            LightSource.pointLightOuterRadius = Mathf.Lerp(LightSource.pointLightOuterRadius, 0.75f, _activationDuration);
            LightSource.color = Color.Lerp(LightSource.color, Color.red, _activationDuration);
            yield return null;
        }
    }
}
