using UnityEngine;

public abstract class ShopItemData : ScriptableObject
{
    [Header("Base Info")]
    public string id;
    public string itemName;
    public Sprite icon;
    public int price;
    public CurrencyType currency;

    public abstract ItemCategory Category { get; }
}
