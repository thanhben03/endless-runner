using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    // DATA LOCAL RUN
    [SerializeField] private int coin;
    [SerializeField] private int gold;
    [SerializeField] private int health;
    [SerializeField] private int distance;

    public int Coin => coin;
    public int Gold => gold;
    public int Health => health;
    public int Distance => distance;

    // DATA SYNC
    private PlayerModel player;

    public PlayerModel Player => player;

    //[SerializeField] private int totalCoin;
    //[SerializeField] private int totalGold;
    //[SerializeField] private int totalDistance;
    //public int TotalCoin => totalCoin;
    //public int TotalGold => totalGold;
    //public int TotalHealth => health;
    //public int TotalDistance => totalDistance;


    public event Action<int> OnCoinChanged;
    public event Action<int> OnGoldChanged;

    public event Action<int> OnHitDamaged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnDistanceChanged;

    private void Awake()
    {
        ResetPlayerData();
        if (Instance == null)
        {
            //LoadMetaData();

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayerId") > 0)
        {

            SyncPlayerFromServer();
        }
    }

    //void LoadMetaData()
    //{

    //    totalCoin = PlayerPrefs.GetInt("TOTAL_COIN", 0);
    //    totalGold = PlayerPrefs.GetInt("TOTAL_GOLD", 0);
    //}

    public void AddCoint(int num)
    {
        coin += num;
        OnCoinChanged?.Invoke(coin);
    }

    public void HitDamage(int damage)
    {
        health -= damage;
        Debug.Log("Get damage: " + damage);
        OnHitDamaged?.Invoke(health);
    }

    public void AddHeath()
    {
        health++;
        OnHealthChanged?.Invoke(health);
    }

    public void AddGold()
    {
        gold++;
        OnGoldChanged?.Invoke(gold);
    }

    public void UpdateDistance(int newDis)
    {
        distance += newDis;
        // gui distance de tinh toan score
        ScoreManager.Instance.UpdateDistanceScore(distance);
        OnDistanceChanged?.Invoke(distance);
    }

    public void ResetPlayerData()
    {
        coin = 0;
        gold = 0;
        health = 3;
        distance = 0;

        OnCoinChanged?.Invoke(coin);
        OnGoldChanged?.Invoke(gold);
        OnHealthChanged?.Invoke(health);
        OnDistanceChanged?.Invoke(distance);
    }

    public void SaveAfterRun()
    {
        player.coin += coin;
        player.gold += gold;
        player.distance += distance;
        player.score += ScoreManager.Instance.TotalScore;
        UpdatePlayerRequest req = new UpdatePlayerRequest
        {
            coin = player.coin,
            gold = player.gold,
            distance = player.distance,
            score = player.score
        };

        StartCoroutine(PlayerAPI.Instance.UpdatePlayer(
            player.id, 
            req,
            onSuccess: () =>
            {
                Debug.Log("UPDATE PLAYER: " + req.coin + " / " + req.gold + " / " + req.score);
            },
            onError: message =>
            {
                Debug.LogError("ERROR WHEN UPDATE PLAYER: " + message);
            }
            )
        );

        //PlayerPrefs.SetInt("TOTAL_COIN", player.coin);
        //PlayerPrefs.SetInt("TOTAL_GOLD", player.gold);
        //PlayerPrefs.Save();
    }

    public void SaveCurrency(CurrencyType type, int amount)
    {
        if (amount <= 0)
            return;
        switch (type)
        {
           
            case CurrencyType.Coin:
                player.coin += amount;
                PlayerPrefs.SetInt("TOTAL_COIN", player.coin);
                break;

            case CurrencyType.Gold:
                player.gold += amount;
                PlayerPrefs.SetInt("TOTAL_GOLD", gold);
                break;
        }
    }

    public bool TrySpend(CurrencyType type, int amount)
    {
        switch (type)
        {
            case CurrencyType.Coin:
                if (player.coin < amount) return false;
                player.coin -= amount;
                PlayerPrefs.SetInt("TOTAL_COIN", player.coin);
                break;

            case CurrencyType.Gold:
                if (player.gold < amount) return false;
                player.gold -= amount;
                PlayerPrefs.SetInt("TOTAL_GOLD", player.gold);
                break;
        }

        PlayerPrefs.Save();
        return true;
    }

    public void SaveNewPlayer(int id, string nickname)
    {
        PlayerPrefs.SetInt("PlayerId", id);
        PlayerPrefs.SetString("Nickname", nickname);
        PlayerPrefs.Save();
    }

    public void SyncPlayerFromServer()
    {
        int playerId = PlayerPrefs.GetInt("PlayerId");
        Debug.Log("PLAYER ID: " + playerId);
        StartCoroutine(PlayerAPI.Instance.SyncPlayerFromServer(
            playerId,
            onSuccess: playerRes =>
            {
                player = playerRes;
            },
            onError: message =>
            {
                Debug.LogError("error when load player: " + message);
            }
        ));
    }
}
