using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleScoreCollect : MonoBehaviour
{

    public ParticleSystem multiScoreEff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            multiScoreEff.Play();
            PowerUpManager.Instance.Activate(PowerUpType.DoubleScore);
            AudioManager.Instance.PlaySoundCointCollect();
            Destroy(gameObject, 0.2f);
        }
    }
}
