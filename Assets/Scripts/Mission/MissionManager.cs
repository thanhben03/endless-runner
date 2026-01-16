using UnityEngine;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public List<Mission> activeMissions = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadMissions();
    }

    public void AddDistance(int meters)
    {
        foreach (var mission in activeMissions)
        {
            if (mission.type == MissionType.RunDistance && !mission.IsCompleted)
            {
                mission.currentValue += meters;
                PlayerPrefs.SetInt("MISSION_" + mission.id, mission.currentValue);
                PlayerPrefs.Save();
            }
        }
    }

    public void CollectItem(int itemId)
    {
        foreach (var mission in activeMissions)
        {
            if (mission.type == MissionType.CollectItem &&
                mission.item.id == itemId &&
                !mission.IsCompleted)
            {
                mission.currentValue++;
                PlayerPrefs.SetInt("MISSION_" + mission.id, mission.currentValue);
                PlayerPrefs.Save();
            }
        }
    }

    void CheckMission(Mission mission)
    {
        if (mission.IsCompleted)
        {
            Debug.Log($"Hoàn thành nhiệm vụ: {mission.description}");
        }
    }

    public int GetProgressMission(string id)
    {
        return activeMissions.Find(x => x.id == id).currentValue;
    }

    public void LoadMissions()
    {
        foreach (var mission in activeMissions)
        {
            mission.isCollected = PlayerPrefs.GetInt("COMPLETED_MISSION_" + mission.id) == 1 ? true : false;
            mission.currentValue =
                PlayerPrefs.GetInt("MISSION_" + mission.id, 0);
        }
    }

}
