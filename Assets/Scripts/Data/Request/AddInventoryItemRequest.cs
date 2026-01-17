[System.Serializable]
public class AddInventoryItemRequest
{
    public int itemId;
    public ItemCategory itemType;
    public int quantity;
    public int price;
    public CurrencyType currency;
}
