using UnityEngine;

[System.Serializable]
public class Mission
{
    public string id;
    public string description;

    public MissionType type;

    public int targetValue;

    public int currentValue;

    public ShopItemData item;
    public Sprite sprite;

    public bool isCollected;

    public int coinAward;

    public bool IsCompleted => currentValue >= targetValue;

    public void Reset()
    {
        currentValue = 0;
        isCollected = false;
    }


}
