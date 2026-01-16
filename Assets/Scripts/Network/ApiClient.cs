using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiClient
{
    private const string BASE_API_URL = "http://localhost:8080/api/";



    public static IEnumerator Post<TRequest, TResponse>(
        string url,
        TRequest data,
        Action<TResponse> onSuccess,
        Action<string> onError
    )
    {
        string json = JsonUtility.ToJson(data);

        UnityWebRequest req = new UnityWebRequest(BASE_API_URL + url, "POST");
        req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            TryParseError(req.downloadHandler.text, onError);
        }
        else
        {
            try
            {
                TResponse res =
                    JsonUtility.FromJson<TResponse>(req.downloadHandler.text);
                onSuccess?.Invoke(res);
            }
            catch (Exception e)
            {
                onError?.Invoke("Parse response failed");
                Debug.LogError(e);
            }
        }
    }

    private static void TryParseError(string json, Action<string> onError)
    {
        try
        {
            ErrorResponse err = JsonUtility.FromJson<ErrorResponse>(json);
            onError?.Invoke(err.error);
        }
        catch
        {
            onError?.Invoke("Unknown server error");
        }
    }
}
