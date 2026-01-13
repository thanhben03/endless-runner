using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointCollect : MonoBehaviour
{
    public AudioClip cointCollectSound;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundCointCollect();
            PlayerDataManager.Instance.AddCoint(1);
            ScoreManager.Instance.AddItemScore(2);
            Destroy(gameObject);
        }
    }
}
