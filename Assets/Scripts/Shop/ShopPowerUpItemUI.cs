using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPowerUpItemUI : ShopItem
{
    public PUData data;

    private void Start()
    {
        RenderUIItem(data);
    }

    protected override void RenderUIItem(ShopItemData data)
    {
        base.RenderUIItem(data);

        qty.text = InventoryManager.Instance.GetCount(data.id) + "";
    }

    public override void Buy()
    {
        if (!PlayerDataManager.Instance.TrySpend(data.currency, data.price))
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerItemModel playerItemModel = new PlayerItemModel
        {
            category = ItemCategory.PowerUp,
            itemId = data.id,
            quantity = 1
        };

        InventoryManager.Instance.ApplyItem(playerItemModel);
        UpdateQtyItem();
        ShopManager.Instance.UpdateCurrency();
    }

}
