using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPopup : MonoBehaviour
{
    public Transform content;
    public MissionItem missionItemPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (var mission in MissionManager.Instance.activeMissions)
        {
            var item = Instantiate(missionItemPrefab, content);
            item.SetData(mission);
        }
    }

    public void OnClosePopup()
    {
        gameObject.SetActive(false);
    }
}
