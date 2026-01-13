using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollect : MonoBehaviour
{
    public ParticleSystem magnetEff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            magnetEff.Play();
            PowerUpManager.Instance.Activate(PowerUpType.Magnet);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject);
        }
    }
}
