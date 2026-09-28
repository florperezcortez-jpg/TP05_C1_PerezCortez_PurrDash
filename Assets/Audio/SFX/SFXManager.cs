using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip healthClip;
    [SerializeField] private AudioClip invincibilityClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip gameOverClip;

    // Métodos para el menu principal
    public void PlayButtonClick()
    {
        PlayClip(buttonClickClip);
    }

    // Métodos para el gameplay
    public void PlayJump()
    {
        PlayClip(jumpClip);
    }
    public void PlayCoin()
    {
        PlayClip(coinClip);
    }

    public void PlayHealth()
    {
        PlayClip(healthClip);
    }

    public void PlayInvincibility()
    {
        PlayClip(invincibilityClip);
    }

    public void PlayDie()
    {
        PlayClip(dieClip);
    }

    public void PlayGameOver()
    {
        PlayClip(gameOverClip);
    }
    //auxiliar con playoneshot
    private void PlayClip(AudioClip clip)
    {
        if (sfxAudioSource != null && clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
}