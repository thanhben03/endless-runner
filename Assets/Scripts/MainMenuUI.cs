using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button playBtn;
    public Button shopBtn;
    public Button settingBtn;
    public Button missionBtn;
    public GameObject missionPopup;
    public GameObject settingPopup;
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI yourId;
    void Start()
    {
        playBtn.onClick.AddListener(StartGame);
        shopBtn.onClick.AddListener(LoadShop);
        missionBtn.onClick.AddListener(LoadMission);
        settingBtn.onClick.AddListener(LoadSetting);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);
        UpdateNickName();

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

    private void LoadSetting()
    {
        settingPopup.SetActive(true);
    }

    public void UpdateNickName()
    {
        nickName.text = PlayerPrefs.GetString("Nickname", "");
        yourId.text = "YOUR ID: " + PlayerPrefs.GetInt("PlayerId", 0);
    }
}
