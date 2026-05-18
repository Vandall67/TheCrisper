using System.Collections;
using UnityEngine;

public class TutorialCardAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private float visibleDuration = 4f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    [Header("Pause Game While Visible")]
    [SerializeField] private bool pauseGameWhileVisible = true;

    [Header("Optional Movement")]
    [SerializeField] private bool useScalePop = true;
    [SerializeField] private float startScale = 0.96f;

    [Header("Testing")]
    [SerializeField] private bool enableTestKey = true;
    [SerializeField] private KeyCode testKey = KeyCode.T;

    private CanvasGroup canvasGroup;
    private Coroutine animationCoroutine;
    private Vector3 originalScale;
    private float previousTimeScale = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalScale = transform.localScale;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (enableTestKey && Input.GetKeyDown(testKey))
        {
            ShowCard();
        }
    }

    public void ShowCard()
    {
        gameObject.SetActive(true);

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        if (pauseGameWhileVisible)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        if (useScalePop)
        {
            transform.localScale = originalScale * startScale;
        }

        yield return Fade(0f, 1f, fadeInDuration);

        transform.localScale = originalScale;

        yield return new WaitForSecondsRealtime(visibleDuration);

        yield return Fade(1f, 0f, fadeOutDuration);

        transform.localScale = originalScale;

        if (pauseGameWhileVisible)
        {
            Time.timeScale = previousTimeScale;
        }

        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = timer / duration;

            canvasGroup.alpha = Mathf.Lerp(from, to, t);

            if (useScalePop && to > from)
            {
                transform.localScale = Vector3.Lerp(
                    originalScale * startScale,
                    originalScale,
                    t
                );
            }

            yield return null;
        }

        canvasGroup.alpha = to;
    }
}