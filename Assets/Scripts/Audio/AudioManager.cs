using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Sound[] musicSounds, effectsSounds;
    [SerializeField] private AudioSource musicSource, effectsSource;

    public float musicVolume { get; private set; }
    public float effectsVolume { get; private set; }

    public bool isMusicMuted { get; private set; }

    public bool isEffectsMuted { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        StartCoroutine(SetMixerSettings());
        PlayMusic("Background");
    }

    private IEnumerator SetMixerSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        isMusicMuted = PlayerPrefs.GetInt("isMusicMuted", 0) == 1;
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1);
        isEffectsMuted = PlayerPrefs.GetInt("isEffectsMuted", 0) == 1;

        yield return new WaitForEndOfFrame();

        if (isMusicMuted)
            MuteMusic();
        else
            UnmuteMusic();

        if (isEffectsMuted)
            MuteEffects();
        else
            UnmuteEffects();
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 0f || volume > 1f) 
            throw new ArgumentException($"source volume should be on interval from 0 to 1, not {volume}");

        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        if (!isMusicMuted)
            audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void MuteMusic()
    {
        audioMixer.SetFloat("MusicVolume", -80f);
        isMusicMuted = true;
        PlayerPrefs.SetInt("isMusicMuted", 1);
    }

    public void UnmuteMusic()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, musicVolume));
        isMusicMuted = false;
        PlayerPrefs.SetInt("isMusicMuted", 0);
    }

    public void SetEffectsVolume(float volume)
    {
        if (volume < 0f || volume > 1f)
            throw new ArgumentException($"source volume should be on interval from 0 to 1, not {volume}");

        effectsVolume = volume;
        PlayerPrefs.SetFloat("EffectsVolume", volume);
        if (!isEffectsMuted)
            audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void MuteEffects()
    {
        audioMixer.SetFloat("EffectsVolume", -80f);
        isEffectsMuted = true;
        PlayerPrefs.SetInt("isEffectsMuted", 1);
    }

    public void UnmuteEffects()
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, effectsVolume));
        isEffectsMuted = false;
        PlayerPrefs.SetInt("isEffectsMuted", 0);
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
            throw new ArgumentException($"music sound named {name} can not be found");
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        Sound sound = Array.Find(musicSounds, x => x.clip == clip);

        if (sound == null)
            throw new ArgumentException($"music sound named {name} can not be found");
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlayEffect(string name)
    {
        Sound sound = Array.Find(effectsSounds, x => x.name == name);

        if (sound == null)
            throw new ArgumentException($"effect sound named {name} can not be found");
        else
        {
            effectsSource.PlayOneShot(sound.clip);
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        Sound sound = Array.Find(effectsSounds, x => x.clip == clip);

        if (sound == null)
            throw new ArgumentException($"effect sound named {name} can not be found");
        else
        {
            effectsSource.PlayOneShot(sound.clip);
        }
    }
}
