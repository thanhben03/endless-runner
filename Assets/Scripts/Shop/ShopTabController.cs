using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabController : MonoBehaviour
{
    [Header("Tabs")]
    public ShopTabUI[] tabs;

    [Header("Contents")]
    public GameObject[] contents;

    [Header("Scroll")]
    public ScrollRect scrollRect;

    int currentIndex = -1;

    void Start()
    {
        SelectTab(0);
    }

    public void SelectTab(int index)
    {
        if (index == currentIndex) return;
        currentIndex = index;

        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(i == index);
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == index);
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
