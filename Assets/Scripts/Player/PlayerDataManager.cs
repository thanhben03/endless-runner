using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public static int coint;
    public static int gold;
    public static int healh = 3;
    public static int distance = 0;
    public event Action<int> OnCointChanged;
    public event Action<int> OnGoldChanged;

    public event Action<int> OnHitDamaged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnDistanceChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoint(int num)
    {
        coint += num;
        OnCointChanged?.Invoke(coint);
    }

    public void HitDamage(int damage)
    {
        healh -= damage;
        OnHitDamaged?.Invoke(healh);
    }

    public void AddHeath()
    {
        healh++;
        OnHealthChanged?.Invoke(healh);
    }

    public void AddGold()
    {
        gold++;
        OnGoldChanged?.Invoke(gold);
    }

    public void UpdateDistance(int newDis)
    {
        distance += newDis;
        OnDistanceChanged?.Invoke(distance);
    }
}
