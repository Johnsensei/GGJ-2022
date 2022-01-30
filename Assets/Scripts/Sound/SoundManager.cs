using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Setup")]
    public AudioSource AudioEmitter;

    [Header("Game Music")]
    public AudioClip GameMusic;
    public float GameMusicVolume = 0.5f;

    [Header("SFX")]

    public static SoundManager Singleton;

    public void Awake()
    {
        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            Singleton = this;

        DontDestroyOnLoad(gameObject);
    }

    static void PlaySound(AudioClip sound, float volume)
    {
        Singleton?.AudioEmitter?.PlayOneShot(sound, volume);
    }

    public static void PlayGameMusic()
    {
        if(Singleton != null)
            PlaySound(Singleton.GameMusic, Singleton.GameMusicVolume);
    }
}
