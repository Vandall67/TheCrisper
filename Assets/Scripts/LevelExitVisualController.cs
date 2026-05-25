using UnityEngine;

public class LevelExitVisualController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Renderer exitRenderer;

    [Header("Visual State")]
    [SerializeField] private Color lockedColor = new Color(0.25f, 0.25f, 0.35f);
    [SerializeField] private Color unlockedColor = new Color(0.2f, 1f, 0.6f);

    [SerializeField] private bool useEmission = true;
    [SerializeField] private Color emissionColor = new Color(0.2f, 1f, 0.6f);
    [SerializeField] private float emissionIntensityWhenUnlocked = 2f;

    private Material materialInstancia;
    private bool estadoAnteriorMapaCompleto = false;

    private void Start()
    {
        if (exitRenderer != null)
        {
            materialInstancia = exitRenderer.material;
        }

        AtualizarVisual();
    }

    private void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        if (gameManager.MapaCompleto != estadoAnteriorMapaCompleto)
        {
            AtualizarVisual();
        }
    }

    private void AtualizarVisual()
    {
        if (gameManager == null || materialInstancia == null)
        {
            return;
        }

        bool mapaCompleto = gameManager.MapaCompleto;
        estadoAnteriorMapaCompleto = mapaCompleto;

        if (mapaCompleto)
        {
            materialInstancia.color = unlockedColor;

            if (useEmission)
            {
                materialInstancia.EnableKeyword("_EMISSION");
                materialInstancia.SetColor("_EmissionColor", emissionColor * emissionIntensityWhenUnlocked);
            }
        }
        else
        {
            materialInstancia.color = lockedColor;

            if (useEmission)
            {
                materialInstancia.EnableKeyword("_EMISSION");
                materialInstancia.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}