using System.Collections;
using UnityEngine;

/// <summary>
/// Component to destroy entrance screen
/// </summary>
public class SceneEnterCanvas : MonoBehaviour
{
    [SerializeField] private float EntryScreenDisplayTime = 2f;
    
    private void Awake()
    {
        Time.timeScale = 0;
        StartCoroutine(nameof(UnpauseGame));
    }
    
    private IEnumerator UnpauseGame()
    {
        yield return new WaitForSecondsRealtime(EntryScreenDisplayTime);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
