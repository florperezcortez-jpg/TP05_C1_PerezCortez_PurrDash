using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private static AudioSettingsController instance;
    private void Awake()

    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // evita que se borre al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // evita que se duplique si volvés al menú
        }
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(
            "MusicVolume",
            Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f
            );
    }
public void SetSFXVolume (float value)
{
    audioMixer.SetFloat(
        "SFXVolume",
        Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f
        );
}
}