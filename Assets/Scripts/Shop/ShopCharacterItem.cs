using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterItem : ShopItem
{
    public CharacterData data;
    private Button buyBtn;

    private void Start()
    {
        buyBtn = GetComponentInChildren<Button>();
        RenderUIItem(data);
    }

    protected override void RenderUIItem(ShopItemData data)
    {
        base.RenderUIItem(data);
        qty.text = InventoryManager.Instance.GetCount(data.Category, data.id) + "";
        if (InventoryManager.Instance.GetCount(data.Category, data.id) > 0)
        {
            buyBtn.interactable = false;
        }

    }

    public override void Buy()
    {
        if (!PlayerDataManager.Instance.TrySpend(data.currency, data.price))
        {
            Debug.Log("Not enough money!");
            return;
        }

        InventoryManager.Instance.Add(data.Category, data.id);
        UpdateQtyItem();
        ShopManager.Instance.UpdateCurrency();
        buyBtn.interactable = false;
    }
}
