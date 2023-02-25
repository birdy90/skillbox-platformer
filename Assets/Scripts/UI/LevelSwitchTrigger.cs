using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelSwitchTrigger : MonoBehaviour
{
    [SerializeField] private string LevelName;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(LevelName);
    }
}
