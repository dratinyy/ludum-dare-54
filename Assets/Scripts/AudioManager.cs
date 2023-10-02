
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public AudioSource soundSource;
    public List<AudioClip> musicClipsNight;
    public List<AudioClip> musicClipsDay;
    public List<AudioClip> musicClipsEnding;
    public AudioSource musicSource;
    

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Fonction pour jouer un son
    public void PlaySound(AudioClip soundClip, float volume = 1.0f)
    {
        soundSource.volume = volume;
        soundSource.PlayOneShot(soundClip);
    }

    // muisc for night
    public void PlayRandomMusicNight(float volume = 1.0f)
    {
        if (musicClipsNight.Count > 0)
        {
            int randomIndex = Random.Range(0, musicClipsNight.Count);
            AudioClip randomClip = musicClipsNight[randomIndex];
            musicSource.volume = volume;
            musicSource.clip = randomClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucune musique dans la liste.");
        }
    }
    // muisc for day
    public void PlayRandomMusicDay(float volume = 1.0f)
    {
        if (musicClipsDay.Count > 0)
        {
            int randomIndex = Random.Range(0, musicClipsDay.Count);
            AudioClip randomClip = musicClipsDay[randomIndex];
            musicSource.volume = volume;
            musicSource.clip = randomClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucune musique dans la liste.");
        }
    }
    public void PlayRandomMusicEnding(float volume = 1.0f)
    {
        if (musicClipsDay.Count > 0)
        {
            int randomIndex = Random.Range(0, musicClipsEnding.Count);
            AudioClip randomClip = musicClipsEnding[randomIndex];
            musicSource.volume = volume;
            musicSource.clip = randomClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucune musique dans la liste.");
        }
    }

    // Fonction pour jouer de la musique
    public void PlayMusic(AudioClip musicClip, float volume = 1.0f)
    {
        musicSource.volume = volume;
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Fonction pour arrêter la musique
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Fonction pour régler le volume de la musique
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Fonction pour régler le volume des sons
    public void SetSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }
}
