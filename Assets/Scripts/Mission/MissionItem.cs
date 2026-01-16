using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionItem : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;
    public Image icon;
    public Button collectBtn;
 

    public void SetData(Mission mission)
    {
        collectBtn.onClick.AddListener(() => GetAward(mission));
        icon.sprite = mission.type == MissionType.CollectItem ? mission.item.icon : mission.sprite;
        descriptionText.text = mission.description;
        progressText.text =
            $"{mission.currentValue}/{mission.targetValue}";

        collectBtn.interactable = mission.IsCompleted && !mission.isCollected;
    }

    public void GetAward(Mission mission)
    {
        mission.isCollected = true;
        collectBtn.interactable = false;
        PlayerDataManager.Instance.SaveCurrency(CurrencyType.Coin, mission.coinAward);
        PlayerPrefs.SetInt("COMPLETED_MISSION_" + mission.id, 1);
    }
}
