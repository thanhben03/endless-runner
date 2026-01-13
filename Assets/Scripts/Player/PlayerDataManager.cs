using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    [SerializeField] private int coin;
    [SerializeField] private int gold;
    [SerializeField] private int health;
    [SerializeField] private int distance;

    public int Coin => coin;
    public int Gold => gold;
    public int Health => health;
    public int Distance => distance;

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
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoint(int num)
    {
        coin += num;
        OnCoinChanged?.Invoke(coin);
    }

    public void HitDamage(int damage)
    {
        health -= damage;
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

}
