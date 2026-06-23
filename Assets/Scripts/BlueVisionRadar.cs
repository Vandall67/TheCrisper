using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueVisionRadar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BlueVisionController blueVisionController;
    [SerializeField] private Transform player;
    [SerializeField] private Transform directionReference;

    [Header("UI")]
    [SerializeField] private RectTransform radarRoot;
    [SerializeField] private RectTransform iconsParent;
    [SerializeField] private Image fragmentIconPrefab;

    [Header("Radar Settings")]
    [SerializeField] private float radarRadius = 80f;
    [SerializeField] private float maxWorldDistance = 40f;
    [SerializeField] private bool rotateWithReference = true;

    [Header("Visibility")]
    [SerializeField] private CanvasGroup radarCanvasGroup;
    [SerializeField] private float visibleAlpha = 1f;
    [SerializeField] private float hiddenAlpha = 0f;

    private readonly List<FragmentoMapa> fragments = new();
    private readonly List<Image> fragmentIcons = new();

    private void Start()
    {
        CacheFragments();
        CreateIcons();
        SetRadarVisible(false);
    }

    private void Update()
    {
        if (blueVisionController == null || player == null || radarRoot == null)
        {
            SetRadarVisible(false);
            return;
        }

        bool radarActive = blueVisionController.IsBlueVisionActive;
        SetRadarVisible(radarActive);

        if (!radarActive)
        {
            return;
        }

        UpdateIcons();
    }

    private void CacheFragments()
    {
        fragments.Clear();

        FragmentoMapa[] foundFragments = FindObjectsByType<FragmentoMapa>(FindObjectsSortMode.None);

        foreach (FragmentoMapa fragment in foundFragments)
        {
            if (fragment != null)
            {
                fragments.Add(fragment);
            }
        }
    }

    private void CreateIcons()
    {
        if (fragmentIconPrefab == null || iconsParent == null)
        {
            return;
        }

        foreach (Image icon in fragmentIcons)
        {
            if (icon != null)
            {
                Destroy(icon.gameObject);
            }
        }

        fragmentIcons.Clear();

        for (int i = 0; i < fragments.Count; i++)
        {
            Image icon = Instantiate(fragmentIconPrefab, iconsParent);
            icon.gameObject.SetActive(false);
            fragmentIcons.Add(icon);
        }
    }

    private void UpdateIcons()
    {
        for (int i = 0; i < fragments.Count; i++)
        {
            FragmentoMapa fragment = fragments[i];
            Image icon = fragmentIcons[i];

            if (fragment == null || icon == null || !fragment.gameObject.activeInHierarchy)
            {
                icon.gameObject.SetActive(false);
                continue;
            }

            Vector3 worldOffset = fragment.transform.position - player.position;
            Vector2 radarPosition = ConvertWorldOffsetToRadarPosition(worldOffset);

            float distance = new Vector2(worldOffset.x, worldOffset.z).magnitude;
            float normalizedDistance = Mathf.Clamp01(distance / maxWorldDistance);

            icon.rectTransform.anchoredPosition = radarPosition;
            icon.gameObject.SetActive(true);

            Color iconColor = icon.color;
            iconColor.a = Mathf.Lerp(1f, 0.35f, normalizedDistance);
            icon.color = iconColor;
        }
    }

    private Vector2 ConvertWorldOffsetToRadarPosition(Vector3 worldOffset)
    {
        Vector3 flatOffset = new Vector3(worldOffset.x, 0f, worldOffset.z);

        if (rotateWithReference && directionReference != null)
        {
            Quaternion inverseRotation = Quaternion.Inverse(Quaternion.Euler(0f, directionReference.eulerAngles.y, 0f));
            flatOffset = inverseRotation * flatOffset;
        }

        Vector2 radarDirection = new Vector2(flatOffset.x, flatOffset.z);

        float worldDistance = radarDirection.magnitude;
        float normalizedDistance = Mathf.Clamp01(worldDistance / maxWorldDistance);

        if (radarDirection.sqrMagnitude > 0.001f)
        {
            radarDirection.Normalize();
        }

        return radarDirection * radarRadius * normalizedDistance;
    }

    private void SetRadarVisible(bool isVisible)
    {
        if (radarCanvasGroup != null)
        {
            radarCanvasGroup.alpha = isVisible ? visibleAlpha : hiddenAlpha;
            radarCanvasGroup.interactable = false;
            radarCanvasGroup.blocksRaycasts = false;
        }
        else if (radarRoot != null)
        {
            radarRoot.gameObject.SetActive(isVisible);
        }
    }
}