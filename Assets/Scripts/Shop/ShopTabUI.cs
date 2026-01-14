using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTabUI : MonoBehaviour
{
    public Image bg;
    public TextMeshProUGUI label;

    public Sprite normalSprite;
    public Sprite activeSprite;

    public Color normalTextColor = Color.gray;
    public Color activeTextColor = Color.black;

    private void Start()
    {
        bg = GetComponent<Image>();
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetActive(bool active)
    {
        bg.sprite = active ? activeSprite : normalSprite;
        label.color = active ? activeTextColor : normalTextColor;
    }
}
