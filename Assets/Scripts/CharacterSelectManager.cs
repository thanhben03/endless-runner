using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public List<CharacterData> characters;
    public List<CharacterData> selectableCharacters;

    private int currentIndex = 0;
    private GameObject currentSelectObj;


    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            Init();
        }
    }

    void Start()
    {
        InventoryManager.Instance.OnLoadedInventory += Init;
    }



    private void OnDisable()
    {
        InventoryManager.Instance.OnLoadedInventory -= Init;

    }

    void Init()
    {
        UpdateUI();
        selectableCharacters = characters.FindAll(item =>
        {
            Debug.Log("FIND CHARACTER ID: " + item.id);
            return InventoryManager.Instance.HasItem(item.id);
            
        });
    }


    public void Next()
    {
        if (selectableCharacters.Count == 0) return;
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

        bool owned = selectableCharacters.Count > 0 
            ? InventoryManager.Instance.HasItem(characters[currentIndex].id)
            : true;
        if (currentSelectObj != null)
        {
            Destroy(currentSelectObj);

        }
        if (owned)
        {
            currentSelectObj = Instantiate(characters[currentIndex].prefabMenu);
            currentSelectObj.transform.SetParent(gameObject.transform, false);
            PlayerPrefs.SetInt("CHARACTER_ID", characters[currentIndex].id);
        } else
        {
            Debug.Log("Chua so huu");
        }

    }
}
