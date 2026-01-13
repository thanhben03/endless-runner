using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip slapHurt;
    public AudioClip cointCollect;
    public AudioClip heartAlert;
    public AudioClip countdown;

    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundCointCollect()
    {
        audioSource.PlayOneShot(cointCollect);
    }

    public void PlaySoundSlapHurt()
    {
        audioSource.PlayOneShot(slapHurt);
    }

    public void PlaySoundHeartCollect ()
    {
        audioSource.PlayOneShot(heartAlert);
    }

    public void PlaySoundCountDown()
    {
        audioSource.PlayOneShot(countdown);
    }

}
