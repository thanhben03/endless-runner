using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuControl : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Run");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Instance.pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.pauseMenu.SetActive(false);
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
        UIManager.Instance.gameObject.SetActive(false);
    }

}
