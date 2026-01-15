using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuControl : MonoBehaviour
{
    public static GameMenuControl Instance;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Run");
        //GamePlayManager.Instance.InitPlayer();
        //PlayerDataManager.Instance.ResetPlayerData();
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        //UIManager.Instance.gameObject.SetActive(false);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");

    }

}
