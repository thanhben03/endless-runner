using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDataManager.Instance.AddGold();
            ScoreManager.Instance.AddItemScore(100);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 1f);
        }
    }
}
