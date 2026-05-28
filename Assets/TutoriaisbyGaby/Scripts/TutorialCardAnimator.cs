using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialCardAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private float visibleDuration = 4f; // usado só se dismissByKey = false

    [Header("Dismiss by Key")]
    [SerializeField] private bool dismissByKey = true;
    [SerializeField] private KeyCode dismissKey = KeyCode.E;
    [SerializeField] private TMP_Text dismissPromptText;

    [Header("Optional Movement")]
    [SerializeField] private bool useScalePop = true;
    [SerializeField] private float startScale = 0.96f;

    [Header("Testing")]
    [SerializeField] private bool enableTestKey = true;
    [SerializeField] private KeyCode testKey = KeyCode.T;

    private CanvasGroup canvasGroup;
    private Coroutine animationCoroutine;
    private Vector3 originalScale;
    private bool waitingForDismiss = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        originalScale = transform.localScale;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (dismissPromptText != null)
            dismissPromptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enableTestKey && Input.GetKeyDown(testKey))
            ShowCard();

        if (waitingForDismiss && Input.GetKeyDown(dismissKey))
            DismissCard();
    }

    public void ShowCard()
    {
        // Pede permissão ao TutorialManager
        if (TutorialManager.Instance != null && !TutorialManager.Instance.RequestShow(this))
        {
            // Já há um card aberto — ignora
            return;
        }

        gameObject.SetActive(true);

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(ShowRoutine());
    }

    public void DismissCard()
    {
        if (!waitingForDismiss) return;

        waitingForDismiss = false;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(DismissRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        waitingForDismiss = false;

        if (useScalePop)
            transform.localScale = originalScale * startScale;

        yield return Fade(0f, 1f, fadeInDuration);
        transform.localScale = originalScale;

        if (dismissByKey)
        {
            if (dismissPromptText != null)
                dismissPromptText.gameObject.SetActive(true);

            waitingForDismiss = true;

            while (waitingForDismiss)
                yield return null;
        }
        else
        {
            yield return new WaitForSecondsRealtime(visibleDuration);
            yield return Fade(1f, 0f, fadeOutDuration);
            HideCard();
        }
    }

    private IEnumerator DismissRoutine()
    {
        if (dismissPromptText != null)
            dismissPromptText.gameObject.SetActive(false);

        yield return Fade(1f, 0f, fadeOutDuration);
        HideCard();
    }

    private void HideCard()
    {
        transform.localScale = originalScale;
        canvasGroup.alpha = 0f;
        waitingForDismiss = false;

        if (dismissPromptText != null)
            dismissPromptText.gameObject.SetActive(false);

        // Notifica o manager que este card fechou
        if (TutorialManager.Instance != null)
            TutorialManager.Instance.NotifyCardClosed(this);

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