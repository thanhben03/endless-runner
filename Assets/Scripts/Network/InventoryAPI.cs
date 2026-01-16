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

    public IEnumerator SycnInventory(
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
