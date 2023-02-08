using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DieCanvasController : MonoBehaviour
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}