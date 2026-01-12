using System;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    private float startZ;
    private float distance;

    private float timer;
    private const float UPDATE_INTERVAL = 1f;

    void Start()
    {
        startZ = transform.position.z;
    }

    void Update()
    {
        distance = Mathf.Max(0, transform.position.z - startZ);

        timer += Time.deltaTime;

        if (timer >= UPDATE_INTERVAL)
        {
            timer = 0f;

            int roundedDistance = Mathf.FloorToInt(distance);
            PlayerDataManager.Instance.UpdateDistance(roundedDistance);
        }
    }

    public int GetDistance()
    {
        return Mathf.FloorToInt(distance);
    }

    public void ResetDistance()
    {
        startZ = transform.position.z;
        distance = 0f;
        timer = 0f;
    }
}
