using System;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    private float startZ;
    private float distance;

    private float timer;
    private const float UPDATE_INTERVAL = 3f;


    private PlayerMovement playerMovement;

    void Start()
    {
        startZ = transform.position.z;
        playerMovement = GamePlayManager.Instance.GetPlayer().GetComponent<PlayerMovement>();
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= UPDATE_INTERVAL && playerMovement.forwardSpeed > 0)
        {
            distance = Mathf.Max(0, transform.position.z - startZ ) / 3;
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
