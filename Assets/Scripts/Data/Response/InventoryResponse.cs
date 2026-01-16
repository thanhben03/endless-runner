using System.Collections.Generic;

[System.Serializable]
public class InventoryResponse
{
    public int playerId;
    public List<PlayerItemModel> items;
}
