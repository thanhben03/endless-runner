using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI cointText;
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI distanceText;

    private void Start()
    {
        PlayerDataManager.Instance.OnCointChanged += UpdateUICoint;
        PlayerDataManager.Instance.OnGoldChanged += UpdateUIGold;
        PlayerDataManager.Instance.OnDistanceChanged += UpdateUIDistance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUICoint(int num)
    {
        cointText.text = num + "";
    }

    public void UpdateUIGold(int num)
    {
        goldText.text = num + "";
    }

    public void UpdateUIDistance(int distance)
    {
        distanceText.text = distance + "m";
    }
}
