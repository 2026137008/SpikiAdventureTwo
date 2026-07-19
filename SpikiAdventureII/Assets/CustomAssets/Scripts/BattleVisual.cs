using System.Collections;
using UnityEngine;

public class BattleVisual : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("피격 효과")]
    public float flashTime = 0.2f;

    [Header("사망 효과")]
    public float fadeTime = 1f;

    private Color originalColor;
    private Coroutine flashCoroutine;
    private bool isDead = false;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void FlashRed()
    {
        if (isDead)
            return;

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;

        float t = 0f;

        while (t < flashTime)
        {
            t += Time.deltaTime;

            spriteRenderer.color = Color.Lerp(Color.red, originalColor, t / flashTime);

            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    public IEnumerator FadeOut()
    {
        isDead = true;

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        Color c = spriteRenderer.color;

        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;

            c.a = Mathf.Lerp(1f, 0f, elapsed / fadeTime);

            spriteRenderer.color = c;

            yield return null;
        }

        c.a = 0f;
        spriteRenderer.color = c;
    }
}