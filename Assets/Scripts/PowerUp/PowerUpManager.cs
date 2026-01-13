using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    public List<PowerUpData> powerUps;
    public PowerUpProgressUI progressUI;

    public Magnet magnet;
    public Invincible invincible;
    public MultipleScore multipleScore;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

        foreach (var pu in powerUps)
        {
            if (!pu.isActive) continue;

            pu.remainingTime -= Time.deltaTime;
            progressUI.UpdateBar(pu.type, pu.remainingTime / pu.duration);

            if (pu.remainingTime <= 0)
            {
                Deactivate(pu.type);
            }
        }
    }

    public void Activate(PowerUpType type)
    {
        var pu = powerUps.Find(x => x.type == type);

        if (pu.isActive)
        {
            
            pu.remainingTime = pu.duration;
        }
        else
        {
            pu.isActive = true;
            pu.remainingTime = pu.duration;
            progressUI.Show(type);
        }

        switch (type)
        {
            case PowerUpType.Magnet:
                magnet.ActivateMagnet(pu.duration);
                break;
            case PowerUpType.Invincible:
                invincible.ActivateInvincible(pu.duration);
                break;
            case PowerUpType.DoubleScore:
                multipleScore.ActivateMultipleScore(pu.duration);
                break;
            default:
                break;
        }
    }

    void Deactivate(PowerUpType type)
    {
        var pu = powerUps.Find(x => x.type == type);
        pu.isActive = false;
        progressUI.Hide(type);
    }
}
