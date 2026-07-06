using System.Collections;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public float clickDelay = 0.2f;

    bool clicked = false;

    public void StartGame()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Part1Chapter");
    }

    public void GuideGame()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(GuideGameRoutine());
    }

    IEnumerator GuideGameRoutine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Guide");
    }

    public void QuitGame()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}