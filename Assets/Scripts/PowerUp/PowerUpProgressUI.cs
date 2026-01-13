using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PowerUpProgressUI : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpBar
    {
        public PowerUpType type;
        public GameObject root;   // cả thanh
        public Image fillImage;   // phần fill
    }

    public List<PowerUpBar> bars;

    void Start()
    {
        // Ẩn hết khi bắt đầu game
        foreach (var bar in bars)
        {
            bar.root.SetActive(false);
            bar.fillImage.fillAmount = 0f;
        }
    }

    public void Show(PowerUpType type)
    {
        var bar = GetBar(type);
        if (bar == null) return;

        bar.root.SetActive(true);
        bar.fillImage.fillAmount = 1f;
    }

    public void UpdateBar(PowerUpType type, float percent)
    {
        var bar = GetBar(type);
        if (bar == null) return;

        bar.fillImage.fillAmount = Mathf.Clamp01(percent);
    }

    public void Hide(PowerUpType type)
    {
        var bar = GetBar(type);
        if (bar == null) return;

        bar.root.SetActive(false);
        bar.fillImage.fillAmount = 0f;
    }

    PowerUpBar GetBar(PowerUpType type)
    {
        return bars.Find(b => b.type == type);
    }
}
