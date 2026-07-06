using System.Collections;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public float clickDelay = 0.2f;

    bool clicked = false;

    public void Stage1()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage1Routine());
    }

    IEnumerator Stage1Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("TutorialDialogue");
    }

    public void Stage2()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage2Routine());
    }

    IEnumerator Stage2Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Stage1Dialogue");
    }

    public void Stage3()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage3Routine());
    }

    IEnumerator Stage3Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Stage2Dialogue");
    }

    public void Stage4()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage4Routine());
    }

    IEnumerator Stage4Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Stage3Dialogue");
    }

    public void Stage5()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage5Routine());
    }

    IEnumerator Stage5Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Stage4Dialogue");
    }

    public void Stage6()
    {
        if (clicked) return;

        clicked = true;

        StartCoroutine(Stage6Routine());
    }

    IEnumerator Stage6Routine()
    {
        ButtonSoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(clickDelay);

        FadeManager.Instance.LoadScene("Stage5Dialogue");
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