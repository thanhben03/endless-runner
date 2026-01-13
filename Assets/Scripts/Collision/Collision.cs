using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collision : MonoBehaviour
{
    protected Animation anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invincible invincible = other.GetComponentInChildren<Invincible>();
            if (invincible != null && invincible.IsInvincible)
            {
                return;
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            AudioManager.Instance.PlaySoundSlapHurt();
            OnHit();
            //Destroy(gameObject, 3f);
        }
    }

    public abstract void OnHit();
}
