using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    [Header("효과음")]
    public AudioClip damageSound;
    public AudioClip buttonSound;
    public AudioClip textSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayDamage()
    {
        sfxSource.PlayOneShot(damageSound);
    }

    public void PlayButton()
    {
        sfxSource.PlayOneShot(buttonSound);
    }

    public void PlayText()
    {
        sfxSource.PlayOneShot(textSound);
    }

    public void PlayVictory()
    {
        sfxSource.PlayOneShot(victorySound);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(gameOverSound);
    }
}