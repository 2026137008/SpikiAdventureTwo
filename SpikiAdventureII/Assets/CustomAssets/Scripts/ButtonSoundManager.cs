using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public static ButtonSoundManager Instance;

    public AudioSource audioSource;
    public AudioClip clickSound;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}