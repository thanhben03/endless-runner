using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;
    public List<ShopItemData> allItems;


    public List<PowerUpData> powerUps;
    public PowerUpProgressUI progressUI;

    public Magnet magnet;
    public Invincible invincible;
    public MultipleScore multipleScore;

    void Awake()
    {
        Instance = this;
    }


    private void Reset()
    {
        allItems = new List<ShopItemData>(Resources.LoadAll<ShopItemData>("Items"));

    }

    private void Start()
    {
        GameStartCountDown.Instance.OnStartGame += ActivateEquippedPowerUp;
        magnet = GamePlayManager.Instance.GetPlayer().GetComponentInChildren<Magnet>();
        invincible = GamePlayManager.Instance.GetPlayer().GetComponentInChildren<Invincible>();
        multipleScore = GamePlayManager.Instance.GetPlayer().GetComponentInChildren<MultipleScore>();
    }

    void ActivateEquippedPowerUp()
    {
        int powerUpId = PlayerPrefs.GetInt("EQUIPPED_POWERUP", -1);

        if (powerUpId == -1) return;
        ShopItemData shopItemData = allItems.Find(x => x.id == powerUpId && x.Category == ItemCategory.PowerUp);
        PUData item = shopItemData as PUData;
        if (InventoryManager.Instance.TryUse(powerUpId))
        {
            Activate(item.type);
        }

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
