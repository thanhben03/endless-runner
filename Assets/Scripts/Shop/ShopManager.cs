using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public TextMeshProUGUI totalCoint;
    public TextMeshProUGUI totalGold;

    void Awake()
    {
        //PlayerPrefs.SetInt("TOTAL_COIN", 2000);

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void OnBackButton()
    {
        GameMenuControl.Instance.LoadMainMenu();
    }

    private void Start()
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        //totalCoint.text = PlayerPrefs.GetInt("TOTAL_COIN", 0) + "";
        //totalGold.text = PlayerPrefs.GetInt("TOTAL_GOLD", 0) + "";
        totalCoint.text = PlayerDataManager.Instance.Player.coin + "";
        totalGold.text = PlayerDataManager.Instance.Player.gold + "";
    }
}
