using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button ExitButton;
    
    private void Awake()
    {
        StartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level 1");
        });
        ExitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void OnDestroy()
    {
        StartButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
    }
}
