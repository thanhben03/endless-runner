using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ShopItem : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI qty;
    public TextMeshProUGUI price;
    public TextMeshProUGUI itemName;

    protected virtual void RenderUIItem(ShopItemData data)
    {
        img.sprite = data.icon;
        price.text = data.price.ToString();
        itemName.text = data.itemName;
    }
    protected void UpdateQtyItem()
    {
        int num = int.Parse(qty.text) + 1;

        qty.text = "" + num;
    }

    public abstract void Buy();
}
