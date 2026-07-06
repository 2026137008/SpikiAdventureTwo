using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("Fade Settings")]
    public float fadeTime = 0.8f;

    private Image fadeImage;
    private CanvasGroup canvasGroup;

    private bool isFading = false;

    void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateFadeCanvas();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 바뀌면 자동 Fade In
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    void CreateFadeCanvas()
    {
        // Canvas 생성
        GameObject canvasObj = new GameObject("FadeCanvas");
        DontDestroyOnLoad(canvasObj);

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // FadePanel 생성
        GameObject panel = new GameObject("FadePanel");
        panel.transform.SetParent(canvasObj.transform, false);

        RectTransform rt = panel.AddComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        fadeImage = panel.AddComponent<Image>();
        fadeImage.color = Color.black;

        canvasGroup = panel.AddComponent<CanvasGroup>();

        // 시작은 검은 화면
        SetAlpha(1f);

        // 처음에는 클릭 막음
        canvasGroup.blocksRaycasts = true;
    }

    void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            SetAlpha(Mathf.Lerp(start, end, t / fadeTime));

            yield return null;
        }

        SetAlpha(end);
    }

    public IEnumerator FadeIn()
    {
        canvasGroup.blocksRaycasts = true;

        yield return Fade(1f, 0f);

        // ★ 페이드 끝나면 버튼 클릭 가능
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator FadeOut()
    {
        canvasGroup.blocksRaycasts = true;

        yield return Fade(0f, 1f);
    }

    public void LoadScene(string sceneName)
    {
        if (!isFading)
            StartCoroutine(LoadSceneRoutine(sceneName));
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        isFading = true;

        yield return FadeOut();

        yield return SceneManager.LoadSceneAsync(sceneName);

        // OnSceneLoaded에서 FadeIn 실행

        isFading = false;
    }
}