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
        qty.text = InventoryManager.Instance.GetCount(data.id) + "";
        if (InventoryManager.Instance.GetCount(data.id) > 0)
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
        PlayerItemModel playerItemModel = new PlayerItemModel
        {
            category = ItemCategory.Character,
            itemId = data.id,
            quantity = 1
        };
        int playerId = PlayerPrefs.GetInt("PlayerId", -1);
        StartCoroutine(InventoryAPI.Instance.BuyItem(
            playerId,
            new AddInventoryItemRequest
            {
                itemId = playerItemModel.itemId,
                itemType = playerItemModel.category,
                quantity = playerItemModel.quantity,
                price = data.price,
                currency = data.currency
            },
            onSuccess: res =>
            {
                InventoryManager.Instance.ApplyItem(playerItemModel);
                UpdateQtyItem();
                ShopManager.Instance.UpdateCurrency();
                buyBtn.interactable = false;
            },
            onError: message =>
            {
                Debug.LogError("ERROW WHEN BUY PU ITEM: " + message);
            }
            ));

        
    }
}
