using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleScore : MonoBehaviour
{
    public static MultipleScore Instance { get; private set; }

    public ParticleSystem multipleScoreEff;
    public event Action<bool> OnDoubleScore;

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

    public void ActivateMultipleScore(float duration)
    {
        OnDoubleScore?.Invoke(true);
        multipleScoreEff.Play();
        CancelInvoke();
        Invoke(nameof(DeactivateMagnet), duration);
    }

    void DeactivateMagnet()
    {
        multipleScoreEff.Stop();
        OnDoubleScore?.Invoke(false);

    }
}
