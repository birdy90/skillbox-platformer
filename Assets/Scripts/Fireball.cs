using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fireball: MonoBehaviour
{
    [SerializeField] private float Speed = 0.25f;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Light2D Light;
    [SerializeField] private ParticleSystem DestroyParticles;
    [SerializeField] private LayerMask CollisionMask;

    private int _direction = 1;
    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        UpdateVelocity();
    }

    public void SetDirection(int direction)
    {
        _direction = direction;
        UpdateVelocity();

        if (!SpriteRenderer) return;
            
        if (_direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            DestroyParticles.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void UpdateVelocity()
    {
        _rigidBody.velocity = new Vector3(_direction * Speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // delete object on collision with walls
        if ((CollisionMask.value & (1 << col.gameObject.layer)) == 0)
        {
            return;
        }

        _rigidBody.velocity = Vector3.zero;
        Destroy(SpriteRenderer.gameObject);
        DestroyParticles.gameObject.SetActive(true);
        Invoke(nameof(DestroyFireball), DestroyParticles.main.duration);
        StartCoroutine(nameof(TurnOffLight));
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }

    private IEnumerator TurnOffLight()
    {
        while (true)
        {
            Light.intensity = Mathf.Lerp(Light.intensity, 0f, 0.02f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}