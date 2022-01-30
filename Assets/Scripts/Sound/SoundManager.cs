using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Setup")]
    public AudioSource AudioEmitter;
    public AudioSource RobotMovementAudioEmitter;

    [Header("Game Music")]
    public AudioClip GameMusic;
    public float GameMusicVolume = 0.5f;

    [Header("Button")]
    public AudioClip ButtonSound;
    public float ButtonSoundVolume = 0.5f;

    [Header("SFX")]
    public AudioClip BoxPush;
    public float BoxPushVolume = 0.5f;

    [Header("Beast Movement")]
    public AudioClip BeastMovement1;
    public AudioClip BeastMovement2;
    public float BeastMovementVolume = 0.5f;

    [Header("Robot Movement")]
    public AudioClip RobotMovement;
    public float RobotMovementVolume = 0.5f;

    [Header("Robot Battery")]
    public AudioClip RobotNoBattery;
    public float RobotNoBatteryVolume = 0.5f;

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

    public static void PlayRobotMovement()
    {
        if (Singleton != null && Singleton.RobotMovementAudioEmitter != null)
        {
            if (Singleton.RobotMovementAudioEmitter.isPlaying)
                return;

            Singleton.RobotMovementAudioEmitter.Stop();
            Singleton.RobotMovementAudioEmitter.loop = true;
            Singleton.RobotMovementAudioEmitter.volume = Singleton.RobotMovementVolume;
            Singleton.RobotMovementAudioEmitter.clip = Singleton.RobotMovement;
            Singleton.RobotMovementAudioEmitter.Play();
        }
    }

    public static void StopPlayingRobotSound()
    {
        Singleton?.RobotMovementAudioEmitter?.Stop();
    }

    public static void PlayGameMusic()
    {
        if(Singleton != null)
            PlaySound(Singleton.GameMusic, Singleton.GameMusicVolume);
    }

    public static void PlayBoxPush()
    {
        if (Singleton != null)
            PlaySound(Singleton.BoxPush, Singleton.BoxPushVolume);
    }

    public static void PlayBeastMove1()
    {
        if (Singleton != null)
        {
            PlaySound(Singleton.BeastMovement1, Singleton.BeastMovementVolume);
        }
    }
    public static void PlayBeastMove2()
    {
        if (Singleton != null)
        {
            PlaySound(Singleton.BeastMovement2, Singleton.BeastMovementVolume);
        }
    }

    public static void PlayRobotNoBattery()
    {
        if (Singleton != null)
        {
            PlaySound(Singleton.RobotNoBattery, Singleton.RobotNoBatteryVolume);
        }
    }

    public static void PlayButtonSound()
    {
        if (Singleton != null)
        {
            PlaySound(Singleton.ButtonSound, Singleton.ButtonSoundVolume);
        }
    }
}
