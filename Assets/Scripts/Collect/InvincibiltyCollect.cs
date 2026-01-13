using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibiltyCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpManager.Instance.Activate(PowerUpType.Invincible);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 1f);
        }
    }
}
