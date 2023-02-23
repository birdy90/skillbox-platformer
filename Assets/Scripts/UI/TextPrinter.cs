using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPrinter : MonoBehaviour
{
    [SerializeField] private LayerMask TriggerMask;

    private bool _isActivated;
    private string _initialText;
    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _initialText = _textComponent.text;
        _textComponent.text = "";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Layer {col.gameObject.layer}");
        Debug.Log($"Mask {1 << TriggerMask}");
        if ((TriggerMask & (1 << col.gameObject.layer)) == 0)
        {
            return;
        }

        if (!_isActivated)
        {
            _isActivated = true;
            StartCoroutine(nameof(PrintText));
        }
    }

    private IEnumerator PrintText()
    {
        while (_textComponent.text.Length < _initialText.Length)
        {
            if (Time.timeScale == 0) yield return null;
            _textComponent.text = _initialText.Substring(0, _textComponent.text.Length + 1);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
