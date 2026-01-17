using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAPI : MonoBehaviour
{
    public static InventoryAPI Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator GetInventory(
        int playerId,
        Action<List<PlayerItemModel>> onSuccess,
        Action<string> onError
    )
    {
        yield return ApiClient.Get<InventoryResponse>(
            $"player/{playerId}/inventory",
            res => onSuccess?.Invoke(res.items),
            onError
        );
    }

    public IEnumerator BuyItem (
        int playerId,
        AddInventoryItemRequest req,
        Action<AddInventoryResponse> onSuccess,
        Action<string> onError
    )
    {
        Debug.Log("AddInventory Request: " + req.itemType + "/ " + req.itemId +" / " + req.itemId);
        yield return ApiClient.Post<AddInventoryItemRequest, AddInventoryResponse>(
            $"player/{playerId}/inventory/buy",
            req,
            res => onSuccess?.Invoke(res),
            onError
        );
    }

    public IEnumerator UpdateInventoryItem(
        int playerId,
        int itemId,
        UpdateInventoryItem req,
        Action onSuccess,
        Action<string> onError
    )
    {
        yield return ApiClient.Put<UpdateInventoryItem>(
            $"player/{playerId}/inventory/{itemId}",
            req,
            onSuccess,
            onError

        );
    }
}
