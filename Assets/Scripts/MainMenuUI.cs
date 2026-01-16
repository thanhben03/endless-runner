using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button playBtn;
    public Button shopBtn;
    public Button settingBtn;
    public Button missionBtn;
    public GameObject missionPopup;
    void Start()
    {
        playBtn.onClick.AddListener(StartGame);
        shopBtn.onClick.AddListener(LoadShop);
        missionBtn.onClick.AddListener(LoadMission);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);

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

    private void LoadMission()
    {

        missionPopup.SetActive(true);
    }
}
