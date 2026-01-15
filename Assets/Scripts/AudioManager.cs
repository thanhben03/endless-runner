using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static AudioManager Instance { get; private set; }
    [Header("Audio SFX")]
    public AudioClip slapHurt;
    public AudioClip cointCollect;
    public AudioClip heartAlert;
    public AudioClip countdown;

    [Header("Audio Music")]
    public AudioClip menuMusic;
    public AudioClip deathMusic;


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

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMusicDeath()
    {
        if (musicSource.clip == deathMusic) return;

        musicSource.Stop();
        musicSource.clip = deathMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundCointCollect()
    {
        sfxSource.PlayOneShot(cointCollect);
    }

    public void PlaySoundSlapHurt()
    {
        sfxSource.PlayOneShot(slapHurt);
    }

    public void PlaySoundHeartCollect ()
    {
        sfxSource.PlayOneShot(heartAlert);
    }

    public void PlaySoundCountDown()
    {
        sfxSource.PlayOneShot(countdown);
    }

}
