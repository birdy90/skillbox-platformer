using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wait for player to be in and when player press E, then activate the lever
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class LeverController : MonoBehaviour
{
    public GameObject TextCanvas;
    public bool MultipleActivations = false;
    public string HintText = "press to activate";
    
    private SpriteRenderer _spriteRenderer;
    private TextMeshProUGUI _textMesh;
    private bool _playerIsIn;
    private bool _isActivated;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textMesh = TextCanvas.GetComponentInChildren<TextMeshProUGUI>();
        _textMesh.text = $"<b>E</b>\n<size=0.03>{HintText}</size>";
    }

    private void Update()
    {
        if (_isActivated || !_playerIsIn) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!MultipleActivations)
            {
                _isActivated = true;
                TextCanvas.SetActive(false);
            }

            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            OnActivate?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isActivated) return;
        if (other.CompareTag(Constants.PlayerTag))
        {
            _playerIsIn = true;
            TextCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isActivated) return;
        if (other.CompareTag(Constants.PlayerTag))
        {
            _playerIsIn = false;
            TextCanvas.SetActive(false);
        }
    }

    public UnityEvent OnActivate;
}
