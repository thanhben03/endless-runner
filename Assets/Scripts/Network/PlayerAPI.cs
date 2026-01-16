using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerAPI : MonoBehaviour
{
    public static PlayerAPI Instance { get; private set; }

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
    public IEnumerator CreatePlayer(
        string nickname,
        Action onSuccess,
        Action<string> onError
    )
    {
        Debug.Log("Nickname: " + nickname);
        CreatePlayerRequest data = new CreatePlayerRequest
        {
            nickname = nickname,
            coin = 0,
            gold = 0,
            score = 0,
            distance = 0
        };

        yield return ApiClient.Post<CreatePlayerRequest, PlayerResponse>(
            "player",
            data,
            res =>
            {
                PlayerDataManager.Instance.SaveNewPlayer(res.id, res.nickname);
                GameMenuControl.Instance.LoadMainMenu();
                Debug.Log(res);
                onSuccess?.Invoke();
            },
            onError
        );
    }

    public IEnumerator SyncPlayerFromServer(
        int playerId,
        Action<PlayerModel> onSuccess,
        Action<string> onError
    )
    {
        yield return ApiClient.Get<PlayerResponse>(
            $"player/{playerId}",
            res =>
            {
                PlayerModel player = new PlayerModel
                {
                    id = res.id,
                    nickname = res.nickname,
                    coin = res.coin,
                    gold = res.gold,
                    distance = res.distance
                };

                onSuccess?.Invoke(player);
            },
            onError
        );
    }


    public IEnumerator UpdatePlayer(
        int playerId,
        UpdatePlayerRequest data,
        Action onSuccess,
        Action<string> onError
    )
    {
        yield return ApiClient.Put(
            $"player/{playerId}",
            data,
            onSuccess,
            onError
        );
    }



}
