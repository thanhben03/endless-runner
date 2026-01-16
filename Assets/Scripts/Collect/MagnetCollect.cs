using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollect : MonoBehaviour
{
    public ParticleSystem magnetEff;
    public PUData item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MissionManager.Instance.CollectItem(item.id);
            magnetEff.Play();
            PowerUpManager.Instance.Activate(PowerUpType.Magnet);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject);
        }
    }
}
