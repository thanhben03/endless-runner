using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public bool IsInvincible { get; private set; }

    public ParticleSystem invincibleEff;

    public PowerUpData powerUpData;
    public void ActivateInvincible(float duration)
    {
        IsInvincible = true;
        CancelInvoke();
        invincibleEff.Play();
        Invoke(nameof(DeactivateInvincible), duration);
    }

    void DeactivateInvincible()
    {
        IsInvincible = false;
        invincibleEff.Stop();
    }
}
