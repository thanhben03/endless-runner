using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PowerUpSelector : MonoBehaviour
{
    public List<ShopItemData> allItems;

    private List<ShopItemData> selectablePowerUps;
    private int currentIndex = 0;

    public Image imgIcon;
    public TextMeshProUGUI qty;
    public Sprite noItem;

    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            Init();
        }
    }

    void Start()
    {
        InventoryManager.Instance.OnLoadedInventory += Init;
    }



    private void OnDisable()
    {
        InventoryManager.Instance.OnLoadedInventory -= Init;

    }


    private void Init()
    {
        selectablePowerUps = allItems.FindAll(item =>
            item.Category == ItemCategory.PowerUp &&
            InventoryManager.Instance.HasItem(item.id)
        );

        if (selectablePowerUps.Count > 0)
            ShowItem(0);
        else
        {
            imgIcon.sprite = noItem;
            qty.text = "";
        }
    }

    private object WaitUntil(Func<bool> p)
    {
        throw new NotImplementedException();
    }

    public void Next()
    {
        if (selectablePowerUps.Count == 0) return;

        currentIndex = (currentIndex + 1) % selectablePowerUps.Count;
        ShowItem(currentIndex);
    }

    void ShowItem(int index)
    {
        ShopItemData item = selectablePowerUps[index];
        if (item is PUData powerUp)
        {
            imgIcon.sprite = item.icon;
            qty.text = InventoryManager.Instance.GetCount(item.id).ToString();
            PlayerPrefs.SetInt("EQUIPPED_POWERUP", powerUp.id);
        }
        //Debug.Log("Selected PowerUp: " + item.itemName);
        

    }

    public void Prev()
    {
        if (selectablePowerUps.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = selectablePowerUps.Count - 1;

        ShowItem(currentIndex);
    }

    private void Reset()
    {
        allItems = new List<ShopItemData>(Resources.LoadAll<ShopItemData>("Items"));

    }

}
