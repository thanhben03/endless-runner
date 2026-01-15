using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public CharacterData[] characters;

    private int currentIndex = 0;
    private GameObject currentSelectObj;

    void Start()
    {
        MoveToFirstOwned();
        UpdateUI();
    }

    void MoveToFirstOwned()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (InventoryManager.Instance.HasItem(ItemCategory.Character, characters[i].id))
            {
                currentIndex = i;
                return;
            }
        }
    }

    public void Next()
    {
        do
        {
            currentIndex = (currentIndex + 1) % characters.Length;
        }
        while (!InventoryManager.Instance.HasItem(ItemCategory.Character, characters[currentIndex].id));

        UpdateUI();
    }

    public void Prev()
    {
        do
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = characters.Length - 1;
        }
        while (!InventoryManager.Instance.HasItem(ItemCategory.Character, characters[currentIndex].id));

        UpdateUI();
    }

    void UpdateUI()
    {
        bool owned =
            InventoryManager.Instance.HasItem(ItemCategory.Character, characters[currentIndex].id);
        if (currentSelectObj != null)
        {
            Destroy(currentSelectObj);

        }
        if (owned)
        {
            currentSelectObj = Instantiate(characters[currentIndex].prefabMenu);
            currentSelectObj.transform.SetParent(gameObject.transform, false);
            PlayerPrefs.SetInt("CHARACTER_ID", int.Parse(characters[currentIndex].id));

        }

        // Update icon, name

        // Enable / disable Play button
        // playBtn.interactable = owned;
    }
}
