using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    private TutorialCardAnimator activeCard = null;
    private bool isPausedByTutorial = false;

    public bool IsCardActive => activeCard != null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool RequestShow(TutorialCardAnimator card)
    {
        // Se já há um card aberto, recusa
        if (IsCardActive)
            return false;

        activeCard = card;
        PauseBytutorial();
        return true;
    }

    public void NotifyCardClosed(TutorialCardAnimator card)
    {
        if (activeCard != card) return;

        activeCard = null;
        ResumeByTutorial();
    }

    private void PauseBytutorial()
    {
        // Só pausa se o jogo não estava já pausado (ex: pause menu aberto)
        if (Time.timeScale > 0f)
        {
            isPausedByTutorial = true;
            Time.timeScale = 0f;
        }
    }

    private void ResumeByTutorial()
    {
        // Só retoma se fomos nós a pausar
        if (isPausedByTutorial)
        {
            isPausedByTutorial = false;
            Time.timeScale = 1f;
        }
    }
}