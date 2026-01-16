using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    private const string MUSIC_VOL = "Music";
    private const string SFX_VOL = "SFX";


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

    private void Start()
    {
        // Load volume sau khi AudioMixer đã được khởi tạo hoàn toàn
        StartCoroutine(LoadVolumeDelayed());
    }

    private IEnumerator LoadVolumeDelayed()
    {
        // Đợi 1 frame để đảm bảo AudioMixer đã sẵn sàng
        yield return null;
        LoadVolume();
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

    public void SetMusicVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        audioMixer.SetFloat(MUSIC_VOL, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOL, value);
    }

    public void SetSFXVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        audioMixer.SetFloat(SFX_VOL, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SFX_VOL, value);
    }

    public void LoadVolume()
    {
        Debug.Log("Load volumeeee");
        float music = PlayerPrefs.GetFloat(MUSIC_VOL, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_VOL, 1f);

        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }

}
