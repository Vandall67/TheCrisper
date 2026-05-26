using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Tutorial Card")]
    [SerializeField] private TutorialCardAnimator tutorialCard;

    [Header("Show On Start")]
    [SerializeField] private bool showOnStart = false;

    [Header("Trigger Settings")]
    [SerializeField] private bool showOnlyOnce = true;
    [SerializeField] private bool disableObjectAfterTrigger = true;
    [SerializeField] private string playerTag = "Player";

    private bool hasTriggered = false;

    private void Start()
    {
        if (showOnStart && tutorialCard != null)
        {
            hasTriggered = true;
            tutorialCard.ShowCard();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        if (showOnlyOnce && hasTriggered)
            return;

        hasTriggered = true;

        if (tutorialCard != null)
            tutorialCard.ShowCard();

        if (disableObjectAfterTrigger)
            gameObject.SetActive(false);
    }
}