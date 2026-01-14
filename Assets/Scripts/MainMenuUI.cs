using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button playBtn;
    public Button shopBtn;
    void Start()
    {
        playBtn.onClick.AddListener(StartGame);
        shopBtn.onClick.AddListener(LoadShop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame ()
    {
        GameMenuControl.Instance.StartGame();
    }

    private void LoadShop ()
    {
        GameMenuControl.Instance.LoadShop();

    }
}
