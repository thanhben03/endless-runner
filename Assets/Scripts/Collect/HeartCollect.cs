using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDataManager.Instance.AddHeath();
            AudioManager.Instance.PlaySoundHeartCollect();
            Destroy(gameObject);
        }
    }
}
