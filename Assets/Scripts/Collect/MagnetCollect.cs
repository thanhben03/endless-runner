using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PowerUpManager.Instance.Activate(PowerUpType.Magnet);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 1f);
        }
    }
}
