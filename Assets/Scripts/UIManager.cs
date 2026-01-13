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
        PlayerDataManager.Instance.OnCoinChanged += UpdateUICoint;
        PlayerDataManager.Instance.OnGoldChanged += UpdateUIGold;
        PlayerDataManager.Instance.OnDistanceChanged += UpdateUIDistance;
        PlayerDataManager.Instance.OnHitDamaged += UpdateUIResultDeath;
        ScoreManager.Instance.OnScoreChanged += UpdateUIScore;
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

    public void UpdateUICoint(int num)
    {
        cointText.text = num + "";
    }

    public void UpdateUIGold(int num)
    {
        goldText.text = num + "";
    }

    public void UpdateUIDistance(int distance)
    {
        distanceText.text = distance + "m";
    }

    public void UpdateUIScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateUIResultDeath(int health)
    {
        if (health > 0)
        {
            return;
        }
        deathMenu.SetActive(true);
        cointResultText.text = PlayerDataManager.Instance.Coin.ToString();
        goldResultText.text = PlayerDataManager.Instance.Gold.ToString();
        scoreResultText.text = "Score: " + ScoreManager.Instance.TotalScore;
        distanceResultText.text = "Distance: " + PlayerDataManager.Instance.Distance;
    }

    public void UpdateUIPowerUpProgressBar()
    {

    }

}
