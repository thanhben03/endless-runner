using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public List<CharacterData> characters;
    public List<CharacterData> selectableCharacters;

    private int currentIndex = 0;
    private GameObject currentSelectObj;

    void Start()
    {
        UpdateUI();
        selectableCharacters = characters.FindAll(item => InventoryManager.Instance.HasItem(ItemCategory.Character, item.id));
    }


    public void Next()
    {
        if (selectableCharacters.Count == 0) return;
        Debug.Log("Selectable character count: " + selectableCharacters.Count);
        currentIndex = (currentIndex + 1) % selectableCharacters.Count;
        UpdateUI();
    }

    public void Prev()
    {
        if (selectableCharacters.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = selectableCharacters.Count - 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("CURRENT INDEX: " + currentIndex);

        bool owned = selectableCharacters.Count > 0 
            ? InventoryManager.Instance.HasItem(ItemCategory.Character, characters[currentIndex].id)
            : true;
        if (currentSelectObj != null)
        {
            Destroy(currentSelectObj);

        }
        if (owned)
        {
            currentSelectObj = Instantiate(characters[currentIndex].prefabMenu);
            currentSelectObj.transform.SetParent(gameObject.transform, false);
            PlayerPrefs.SetInt("CHARACTER_ID", int.Parse(characters[currentIndex].id));
            Debug.Log("DA SO HUU");
        } else
        {
            Debug.Log("Chua so huu");
        }

    }
}
