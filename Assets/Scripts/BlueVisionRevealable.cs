using UnityEngine;

public class BlueVisionRevealable : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private Renderer[] targetRenderers;

    [Header("Blue Vision Visuals")]
    [SerializeField] private Color blueVisionColor = new Color(0.2f, 0.85f, 1f, 1f);
    [SerializeField] private bool hideWhenBlueVisionIsInactive = false;

    private Material[][] originalMaterials;
    private Material[][] blueVisionMaterials;

    private void Awake()
    {
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<Renderer>(true);
        }

        CacheOriginalMaterials();
        CreateBlueVisionMaterials();
        SetBlueVisionState(false);
    }

    public void SetBlueVisionState(bool isActive)
    {
        if (targetRenderers == null)
        {
            return;
        }

        for (int i = 0; i < targetRenderers.Length; i++)
        {
            Renderer targetRenderer = targetRenderers[i];

            if (targetRenderer == null)
            {
                continue;
            }

            targetRenderer.enabled = isActive || !hideWhenBlueVisionIsInactive;

            if (isActive && blueVisionMaterials != null && i < blueVisionMaterials.Length)
            {
                targetRenderer.materials = blueVisionMaterials[i];
            }
            else if (originalMaterials != null && i < originalMaterials.Length)
            {
                targetRenderer.materials = originalMaterials[i];
            }
        }
    }

    private void CacheOriginalMaterials()
    {
        originalMaterials = new Material[targetRenderers.Length][];

        for (int i = 0; i < targetRenderers.Length; i++)
        {
            if (targetRenderers[i] != null)
            {
                originalMaterials[i] = targetRenderers[i].materials;
            }
        }
    }

    private void CreateBlueVisionMaterials()
    {
        blueVisionMaterials = new Material[targetRenderers.Length][];

        for (int i = 0; i < targetRenderers.Length; i++)
        {
            Renderer targetRenderer = targetRenderers[i];

            if (targetRenderer == null)
            {
                continue;
            }

            Material[] sourceMaterials = targetRenderer.materials;
            blueVisionMaterials[i] = new Material[sourceMaterials.Length];

            for (int j = 0; j < sourceMaterials.Length; j++)
            {
                Material material = new Material(Shader.Find("Standard"));
                material.color = blueVisionColor;
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", blueVisionColor * 1.5f);

                blueVisionMaterials[i][j] = material;
            }
        }
    }
}