using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleScoreCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PowerUpManager.Instance.Activate(PowerUpType.DoubleScore);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 1f);
        }
    }
}
