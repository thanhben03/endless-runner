using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI cointText;
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI scoreText;

    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject countDown;
    public GameObject powerUpProgressBar;

    public TextMeshProUGUI cointResultText;
    public TextMeshProUGUI goldResultText;
    public TextMeshProUGUI scoreResultText;
    public TextMeshProUGUI distanceResultText;

    private void Start()
    {
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.OnCoinChanged += UpdateUICoint;
            PlayerDataManager.Instance.OnGoldChanged += UpdateUIGold;
            PlayerDataManager.Instance.OnDistanceChanged += UpdateUIDistance;
            PlayerDataManager.Instance.OnHitDamaged += UpdateUIResultDeath;
        }
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateUIScore;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe các events để tránh memory leak và lỗi khi object bị destroy
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.OnCoinChanged -= UpdateUICoint;
            PlayerDataManager.Instance.OnGoldChanged -= UpdateUIGold;
            PlayerDataManager.Instance.OnDistanceChanged -= UpdateUIDistance;
            PlayerDataManager.Instance.OnHitDamaged -= UpdateUIResultDeath;
        }
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateUIScore;
        }
    }

    public void UpdateUICoint(int num)
    {
        if (cointText != null)
        {
            cointText.text = num + "";
        }
    }

    public void UpdateUIGold(int num)
    {
        if (goldText != null)
        {
            goldText.text = num + "";
        }
    }

    public void UpdateUIDistance(int distance)
    {
        if (distanceText != null)
        {
            distanceText.text = distance + "m";
        }
    }

    public void UpdateUIScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateUIResultDeath(int health)
    {
        if (health > 0)
        {
            return;
        }
        
        // Kiểm tra null cho tất cả các UI elements trước khi sử dụng
        if (deathMenu == null)
        {
            return;
        }
        
        deathMenu.SetActive(true);
        
        if (cointResultText != null && PlayerDataManager.Instance != null)
        {
            cointResultText.text = PlayerDataManager.Instance.Coin.ToString();
        }
        
        if (goldResultText != null && PlayerDataManager.Instance != null)
        {
            goldResultText.text = PlayerDataManager.Instance.Gold.ToString();
        }
        
        if (scoreResultText != null && ScoreManager.Instance != null)
        {
            scoreResultText.text = "Score: " + ScoreManager.Instance.TotalScore;
        }
        
        if (distanceResultText != null && PlayerDataManager.Instance != null)
        {
            distanceResultText.text = "Distance: " + PlayerDataManager.Instance.Distance;
        }
    }

    public void UpdateUIPowerUpProgressBar()
    {

    }

    public void OnPauseBtn ()
    {
        GameMenuControl.Instance.PauseGame();
    }

    public void LoadMainMenu()
    {
        GameMenuControl.Instance.LoadMainMenu();
    }

    public void OnResumeGame()
    {
        GameMenuControl.Instance.ResumeGame();
    }
}
