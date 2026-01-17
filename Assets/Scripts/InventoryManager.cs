using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    Dictionary<int, PlayerItemModel> inventory = new();
    public event Action OnLoadedInventory;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayerId") > 0)
        {

            SetInventoryFromServer();
        }
    }

    public void SetInventoryFromServer()
    {
        int playerId = PlayerPrefs.GetInt("PlayerId");
        StartCoroutine(InventoryAPI.Instance.GetInventory(
            playerId,
            onSuccess: items =>
            {
                SetInventory(items);
                OnLoadedInventory?.Invoke();
            },
            onError: message =>
            {
                Debug.LogError("ERR WHEN LOAD INVENTORY: " + message);
            }
        ));

    }

    public void SyncInventoryToServer()
    {
        int playerId = PlayerPrefs.GetInt("PlayerId");

    }

    // ===== QUERY =====
    public int GetCount(int itemId)
    {

        return inventory.TryGetValue(itemId, out var item)
            ? item.quantity
            : 0;
    }

    public bool HasItem(int itemId)
    {
        return GetCount(itemId) > 0;
    }

    public void ApplyItem(PlayerItemModel item)
    {
        if (inventory.ContainsKey(item.itemId))
        {
            inventory[item.itemId].quantity++;
            return;
        }
        inventory[item.itemId] = item;
    }

    public void SetInventory(List<PlayerItemModel> items)
    {
        inventory.Clear();
        foreach (var item in items)
        {
            Debug.Log("GET INVENTORY: " + item.itemId + " / " + item.quantity);

            inventory[item.itemId] = item;
        }
    }

    public bool TryUse(int itemId)
    {
        if (!inventory.TryGetValue(itemId, out var item)) return false;
        if (item.quantity <= 0) return false;
        item.quantity--;
        int playerId = PlayerPrefs.GetInt("PlayerId", -1);
        if (playerId == -1)
        {
            return false;
        }
        StartCoroutine(InventoryAPI.Instance.UpdateInventoryItem(
            playerId,
            itemId,
            new UpdateInventoryItem
            {
                quantity = item.quantity
            },
            onSuccess: () =>
            {
                Debug.Log("UPDATE INVENTOY ITEM SUCCESS: " + itemId + "/ " + item.quantity);
            },
            onError: message =>
            {
                Debug.LogError("ERROR WHEN UPDATE INVENTORY ITEM: " + message);

            }
        ));
        return true;
    }

}
