using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int TotalScore { get; private set; }

    private int distanceScore;
    private int itemScore;

    public int distanceMultiplier = 1;
    public int itemMultiplier = 1;
    public event Action<int> OnScoreChanged;

    private bool isDoubleScore = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        MultipleScore.Instance.OnDoubleScore += IsDoubleScore;
    }

    private void IsDoubleScore(bool status)
    {
        isDoubleScore = status;
    }

    public void UpdateDistanceScore(int distance)
    {
        distanceScore = distance * distanceMultiplier;
        CalculateTotalScore();
    }

    public void AddItemScore(int value)
    {
        if (isDoubleScore)
        {
            value *= 2;
            Debug.Log("Double Score");
        }
        itemScore += value * itemMultiplier;
        CalculateTotalScore();
    }

    void CalculateTotalScore()
    {
        TotalScore = distanceScore + itemScore;
        // gui event den ui de cap nhat lai giao dien
        OnScoreChanged?.Invoke(TotalScore);
    }
}
