using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public List<Image> hearts;
    void Start()
    {
        PlayerDataManager.Instance.OnHitDamaged += UpdateHealthBar;
        PlayerDataManager.Instance.OnHealthChanged += UpdateHealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthBar(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].gameObject.SetActive(i < health);

            }
        }
    }
}
