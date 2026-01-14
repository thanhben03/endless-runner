using UnityEngine;
using UnityEngine.UI;

public class ShopTabUI : MonoBehaviour
{
    public Image bg;
    public Text label;

    public Sprite normalSprite;
    public Sprite activeSprite;

    public Color normalTextColor = Color.gray;
    public Color activeTextColor = Color.black;

    public void SetActive(bool active)
    {
        bg.sprite = active ? activeSprite : normalSprite;
        label.color = active ? activeTextColor : normalTextColor;
    }
}
