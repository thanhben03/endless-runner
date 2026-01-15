using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    Dictionary<string, int> inventory = new();


    void Awake()
    {
        //PlayerPrefs.SetInt("TOTAL_COIN", 2000);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
            Add(ItemCategory.Character, "1");
        }
        else Destroy(gameObject);
    }

    private string GetKey(ItemCategory cat, string id)
    {
        return $"{cat}_{id}";
    }

    public int GetCount(ItemCategory cat, string id)
    {
        return inventory.TryGetValue(GetKey(cat, id), out var c) ? c : 0;
    }

    public void Add(ItemCategory cat, string id, int amount = 1)
    {
        inventory[GetKey(cat, id)] = GetCount(cat, id) + amount;
        Save();
    }

    public bool Use(ItemCategory cat, string id)
    {
        if (GetCount(cat, id) <= 0) return false;
        inventory[GetKey(cat, id)]--;
        Save();
        return true;
    }

    void Save()
    {
        foreach (var kv in inventory)
        {
            PlayerPrefs.SetInt("INV_" + kv.Key, kv.Value);
        }

    }

    void Load()
    {
        inventory.Clear();

        var allItems = Resources.LoadAll<ShopItemData>("Items");

        foreach (var item in allItems)
        {
            string key = GetKey(item.Category, item.id);
            inventory[key] = PlayerPrefs.GetInt("INV_" + key, 0);
        }
    }
    public bool HasItem(ItemCategory cat, string id)
    {
        return GetCount(cat, id) > 0;
    }

}
