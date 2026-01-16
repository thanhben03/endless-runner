using UnityEngine;

public abstract class ShopItemData : ScriptableObject
{
    [Header("Base Info")]
    public int id;
    public string itemName;
    public Sprite icon;
    public int price;
    public CurrencyType currency;

    public abstract ItemCategory Category { get; }
}
