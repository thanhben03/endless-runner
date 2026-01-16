using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void OnEnable()
    {
        // Sync slider với giá trị hiện tại khi mở popup
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        
        // Không cần gọi LoadVolume() vì AudioManager đã load trong Start()
    }

    public void OnCloseSettingPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnMusicVolumeChanged(float value)
    {
        Debug.Log("music volume: " + value);
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
