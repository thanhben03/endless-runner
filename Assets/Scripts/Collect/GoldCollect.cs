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
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 2f);
        }
    }
}
