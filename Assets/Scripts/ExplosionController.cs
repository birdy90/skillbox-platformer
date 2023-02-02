using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private Transform ExplosionPoint;
    [SerializeField] private GameObject ExposionPrefab;
    [SerializeField] private float ExplosionDuration = 0.1f;

    private GameObject _bombInstance;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_bombInstance) return;
        
        if (col.gameObject.TryGetComponent(typeof(InputController), out Component component))
        {
            _bombInstance = Instantiate(ExposionPrefab, ExplosionPoint);
            StartCoroutine(nameof(SelfDestroy));
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSecondsRealtime(ExplosionDuration);
        Destroy(gameObject);
    }
}
